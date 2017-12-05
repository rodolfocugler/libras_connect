using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using AForge.Math.Geometry;
using libras_connect_domain.DTO;
using libras_connect_domain.Handler;
using libras_connect_domain.Services.Interfaces;
using System;
using System.Drawing;
using static PXCMImage;

namespace libras_connect_domain.Services.Implements
{
    /// <summary>
    /// Implementation of IRealsenseService <see cref="IRealsenseService"/>
    /// </summary>
    public class RealsenseImageService : BaseRealsesenseService, IRealsenseService, IObserver
    {
        private static bool _isRunning;
        private readonly IDataService _dataService;

        public RealsenseImageService(IDataService dataService)
        {
            _isRunning = true;
            _dataService = dataService;
        }

        /// <summary>
        ///     <para><see cref="IRealsenseService.Start()"/></para>
        /// </summary>
        public void Start()
        {
            try
            {
                using (PXCMSenseManager sm = PXCMSenseManager.CreateInstance())
                {
                    sm.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480, 30);

                    sm.Init();
                    while (_isRunning)
                    {
                        base.CheckError(sm.AcquireFrame(false));

                        PXCMCapture.Sample sample = sm.QuerySample();

                        if (sample != null && sample.color != null)
                        {
                            ImageData imageData = null;

                            base.CheckError(sample.color.AcquireAccess(Access.ACCESS_READ, PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32, out imageData));
                            sample.color.ReleaseAccess(imageData);

                            Bitmap rawBitmap = imageData.ToBitmap(0, sample.color.info.width, sample.color.info.height);

                            Bitmap bitmap = this.ResizeImage(rawBitmap, 32, 24);
                            rawBitmap = this.ResizeImage(rawBitmap, 160, 120);

                            _dataService.OnProcess(bitmap, rawBitmap); 
                        }

                        sm.ReleaseFrame();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler exceptionHandler = new ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Resize an image
        /// </summary>
        /// <param name="bitmap">Image</param>
        /// <param name="width">new width</param>
        /// <param name="height">new height</param>
        /// <returns></returns>
        private Bitmap ResizeImage(Bitmap bitmap, int width, int height)
        {
            ResizeNearestNeighbor filter = new ResizeNearestNeighbor(width, height);
            return filter.Apply(bitmap);
        }

        /// <summary>
        ///     <para><see cref="IObserver.Notify()"/></para>
        /// </summary>
        public void Notify()
        {
            _isRunning = false;
        }
    }
}
