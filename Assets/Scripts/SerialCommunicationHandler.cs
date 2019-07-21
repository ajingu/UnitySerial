using System.IO.Ports;

public delegate void SerialDataReceivedEventHandler(string message);

public class SerialCommunicationHandler
{
    public SerialDataReceivedEventHandler OnDataReceived;

    private SerialPort serialPort;

    public SerialCommunicationHandler(string portName, int baudRate)
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
        }

    }

    public void CloseAndDispose()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
            serialPort.Dispose();
        }
    }

    //[FIXME] can't read data from Arduino Uno (Arduino 'Serial.write(data)' function doesn't work well)
    public void ReadLineAndHandleEvent()
    {
        while (serialPort.IsOpen)
        {
            if (serialPort.BytesToRead > 0)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    OnDataReceived(message);
                }
                catch (System.Exception e)
                {
                    UnityEngine.Debug.Log(e.Message);
                }
            }
        }
    }

    public void Write(string message)
    {
        if (serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(message);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogWarning(e.Message);
            }
        }
    }
}
