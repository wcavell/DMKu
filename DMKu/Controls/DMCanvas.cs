using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using Device = SharpDX.Direct3D10.Device1;
using FeatureLevel = SharpDX.Direct3D10.FeatureLevel;

namespace DMKu.Controls
{
    public class DMCanvas:System.Windows.Controls.Image
    {
        private Device Device;
        private Texture2D RenderTarget;
        private DX10ImageSource D3DSurface;
        private readonly Stopwatch RenderTimer;
        private RenderTarget m_d2dRenderTarget;
        private SharpDX.Direct2D1.Factory m_d2dFactory;
        public DMCanvas()
        {
            this.RenderTimer = new Stopwatch();
            this.Loaded += this.Window_Loaded;
            this.Unloaded += this.Window_Closing;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DMCanvas.IsInDesignMode)
                return;

            this.StartD3D();
            this.StartRendering();
        }

        private void Window_Closing(object sender, RoutedEventArgs e)
        {
            if (DMCanvas.IsInDesignMode)
                return;

            this.StopRendering();
            this.EndD3D();
        }

        private void StartD3D()
        {
            this.Device = new Device(DriverType.Hardware, DeviceCreationFlags.BgraSupport, FeatureLevel.Level_10_0);

            this.D3DSurface = new DX10ImageSource();
            this.D3DSurface.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;

            this.CreateAndBindTargets();

            this.Source = this.D3DSurface;
        }

        private void EndD3D()
        {
            this.D3DSurface.IsFrontBufferAvailableChanged -= OnIsFrontBufferAvailableChanged;
            this.Source = null;

            Disposer.SafeDispose(ref this.m_d2dRenderTarget);
            Disposer.SafeDispose(ref this.m_d2dFactory);
            Disposer.SafeDispose(ref this.D3DSurface);
            Disposer.SafeDispose(ref this.RenderTarget);
            Disposer.SafeDispose(ref this.Device);
        }

        private void CreateAndBindTargets()
        {
            this.D3DSurface.SetRenderTargetDX10(null);

            Disposer.SafeDispose(ref this.m_d2dRenderTarget);
            Disposer.SafeDispose(ref this.m_d2dFactory);
            Disposer.SafeDispose(ref this.RenderTarget);

            int width = Math.Max((int)this.ActualWidth, 100);
            int height = Math.Max((int)this.ActualHeight, 100);

            Texture2DDescription colordesc = new Texture2DDescription
            {
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                Format = Format.B8G8R8A8_UNorm,
                Width = width,
                Height = height,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                OptionFlags = ResourceOptionFlags.Shared,
                CpuAccessFlags = CpuAccessFlags.None,
                ArraySize = 1
            };

            this.RenderTarget = new Texture2D(this.Device, colordesc);

            Surface surface = this.RenderTarget.QueryInterface<Surface>();


            m_d2dFactory = new SharpDX.Direct2D1.Factory();
           // m_dwFactory = new SharpDX.DirectWrite.Factory();
            var rtp = new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied));

            m_d2dRenderTarget = new RenderTarget(m_d2dFactory, surface, rtp);

            this.D3DSurface.SetRenderTargetDX10(this.RenderTarget);
           // format = new SharpDX.DirectWrite.TextFormat(m_dwFactory, "微软雅黑", 16);
        }

        private void StartRendering()
        {
            if (this.RenderTimer.IsRunning)
                return;

            System.Windows.Media.CompositionTarget.Rendering += OnRendering;
            this.RenderTimer.Start();
        }

        private void StopRendering()
        {
            if (!this.RenderTimer.IsRunning)
                return;

            System.Windows.Media.CompositionTarget.Rendering -= OnRendering;
            this.RenderTimer.Stop();
        }

        private void OnRendering(object sender, EventArgs e)
        {
            if (!this.RenderTimer.IsRunning)
                return;

            this.Render();
            this.D3DSurface.InvalidateD3DImage();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.CreateAndBindTargets();
            base.OnRenderSizeChanged(sizeInfo);
        }
        int ie = 0;
        private void Render()
        {
            SharpDX.Direct3D10.Device device = this.Device;
            if (device == null)
                return;

            Texture2D renderTarget = this.RenderTarget;
            if (renderTarget == null)
                return;

            int targetWidth = renderTarget.Description.Width;
            int targetHeight = renderTarget.Description.Height;

            device.Rasterizer.SetViewports(new Viewport(0, 0, targetWidth, targetHeight, 0.0f, 1.0f));

            var rectangleGeometry = new RoundedRectangleGeometry(
              m_d2dFactory, new RoundedRectangle()
              {
                  RadiusX = 32,
                  RadiusY = 32,
                  Rect = new RectangleF(128, 128, targetWidth - 128, targetHeight - 128)
              });
            var solidColorBrush = new SharpDX.Direct2D1.SolidColorBrush(m_d2dRenderTarget, new Color4(1, 1, 1, 20));

            m_d2dRenderTarget.BeginDraw();
            m_d2dRenderTarget.Clear(null);
            solidColorBrush.Color = new Color4(1, 1, 1, (float)Math.Abs(Math.Cos(this.RenderTimer.ElapsedMilliseconds * .001)));
            m_d2dRenderTarget.FillGeometry(rectangleGeometry, solidColorBrush, null);

           // m_d2dRenderTarget.DrawText("我是谁\n可以吗？", format, new RectangleF(20 + ie++, 20, 150, 150), new SolidColorBrush(m_d2dRenderTarget, new Color4(0, 0, 0, 1)));
            m_d2dRenderTarget.EndDraw();
            System.Diagnostics.Debug.WriteLine(i++);
            if (ie >= 400)
                ie = 0;
            device.Flush();
        }
        int i = 0;
        private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // this fires when the screensaver kicks in, the machine goes into sleep or hibernate
            // and any other catastrophic losses of the d3d device from WPF's point of view
            if (this.D3DSurface.IsFrontBufferAvailable)
                this.StartRendering();
            else
                this.StopRendering();
        }

        /// <summary>
        /// Gets a value indicating whether the control is in design mode
        /// (running in Blend or Visual Studio).
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
                bool isDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
                return isDesignMode;
            }
        }
    }
}
