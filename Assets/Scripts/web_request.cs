using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
//using System.Net.NetworkInformation;
using System.Net.NetworkInformation;

public class web_request : MonoBehaviour {

    private const string API_KEY = "AIzaSyCpkgzIx0DLVZd6u15ICaCWaYgR_LfXNTg";
    private const string API_REQUEST_GEOLOCATION = "https://www.googleapis.com/geolocation/v1/geolocate?key=";
    private const string API_REQUEST_MAPS = "https://maps.googleapis.com/maps/api/directions/json?origin=75+9th+Ave+New+York,+NY&destination=MetLife+Stadium+1+MetLife+Stadium+Dr+East+Rutherford,+NJ+07073&key=";

    public Text output_text;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        //        string wifi = "{\"macAddress\": \"98:5F:D3:31:63:69\"}";

//        output_text.text = GetMacAddress();

//        string wifi = "{\"macAddress\": \"98:5F:D3:31:63:69\"}";
//        string json = "{\"considerIp\": \"true\", \"wifiAccessPoints\": ["+wifi+"]}";

//        byte[] payload = System.Text.Encoding.ASCII.GetBytes(json);

///        UploadHandler uploader = new UploadHandlerRaw(payload);
///        // Will send header: "Content-Type: custom/content-type";
///        uploader.contentType = "custom/content-type";

        UnityWebRequest www = new UnityWebRequest(API_REQUEST_MAPS + API_KEY);
///        www.uploadHandler = uploader;
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();

        if (www.isError)
        {
//            Debug.Log(www.error);
            output_text.text += "ERROR: " + www.error;
        }
        else
        {
            // Show results as text
//            Debug.Log(www.downloadHandler.text);
            output_text.text += "DL Handler: " + www.downloadHandler.text;

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            output_text.text += "\nResults: " + www.downloadHandler.data + "\n";
            // Loop through contents of the array.
            foreach (byte element in results)
            {
                output_text.text += (char)element;
            }

        }
    }
/** /
    /// <summary>
    /// Finds the MAC address of the first operation NIC found.
    /// </summary>
    /// <returns>The MAC address.</returns>
    private string GetMacAddress()
    {
//        System.Net.NetworkInformation.IPAddressCollection

        var macAddresses =
        (
            from nic in NetworkInterface.GetAllNetworkInterfaces()
            where nic.OperationalStatus == OperationalStatus.Up
            select nic.GetPhysicalAddress().ToString()
        ).FirstOrDefault();

        return macAddresses;
    }
    public class NetworkInfo
    {
        public static string PhysicalAddress = "";

        public static void DisplayTypeAndAddress()
        {
            //IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            Debug.Log(nics.Length + " nics");
            //Debug.Log("Interface information for "+computerProperties.HostName+"."+computerProperties.DomainName);
            PhysicalAddress = nics[0].GetPhysicalAddress().ToString();
            Debug.Log(PhysicalAddress);
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                /* Debug.Log(adapter.Description);
                 Debug.Log(String.Empty.PadLeft(adapter.Description.Length, '='));
                 Debug.Log("  Interface type .......................... : " + adapter.NetworkInterfaceType);
                 Debug.Log("  Physical Address ........................ : " + adapter.GetPhysicalAddress().ToString());
                 Debug.Log("  Is receive only.......................... : " + adapter.IsReceiveOnly);
                 Debug.Log("  Multicast................................ : " + adapter.SupportsMulticast);* /
            }
        }
    }
/ **/
}

/** /


            string wifi = "{\"macAddress\": \"98:5F:D3:31:63:69\","
            + "\"signalStrength\": -65,"
            + "\"age\": 0,"
            + "\"channel\": 11,"
            + "\"signalToNoiseRatio\": 40";

        string json = "{\"homeMobileCountryCode\": 310,"
            + "\"homeMobileNetworkCode\": 410,"
            + "\"radioType\": \"gsm\","
            + "\"carrier\": \"Vodafone\","
            + "\"considerIp\": \"true\","
            + "\"cellTowers\": [],"
            + "\"wifiAccessPoints\": ["+wifi+"]}";






    byte[] payload = new byte[1024];
// ... fill payload with data ...

UnityWebRequest wr = new UnityWebRequest("http://www.mysite.com/data-upload");
UploadHandler uploader = new UploadHandlerRaw(payload);

// Will send header: "Content-Type: custom/content-type";
uploader.contentType = "custom/content-type";

wr.uploadHandler = uploader;



    {
  "homeMobileCountryCode": 310,
  "homeMobileNetworkCode": 410,
  "radioType": "gsm",
  "carrier": "Vodafone",
  "considerIp": "true",
  "cellTowers": [
    // See the Cell Tower Objects section below.
  ],
  "wifiAccessPoints": [
    // See the WiFi Access Point Objects section below.
  ]
}

/ **/
