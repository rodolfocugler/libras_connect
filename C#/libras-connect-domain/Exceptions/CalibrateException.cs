using libras_connect_domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Exceptions
{
    public class CalibrateException : Exception
    {
        public CameraEnum CameraEnum { get; private set; }

        public CalibrateException(string message) : base(message)
        {
        }

        public CalibrateException(string message, CameraEnum cameraEnum) : base(message)
        {
            this.CameraEnum = cameraEnum;
        }
    }
}
