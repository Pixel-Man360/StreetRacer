public interface IDeviceBackButtonInterface
{
    void PushToStack();
    void PopFromStack();
    void OnDeviceBackButtonPressed();
    bool IsPopableOnDeviceBackBtn();
}
