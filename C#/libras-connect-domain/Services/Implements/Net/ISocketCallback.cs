using libras_connect_domain.Enums;

namespace libras_connect_domain.Services.Implements.Net
{
    /// <summary>
    /// Receive data from socket
    /// </summary>
    public interface ISocketCallback
    {
        void Receive(string text, CameraEnum cameraEnum);
    }
}
