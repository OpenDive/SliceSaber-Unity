using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using WalletConnectSharp;
using WalletConnectSharp.Models;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;

public class Web3Controller : MonoBehaviour
{
    string uri = "";
    public WalletConnect walletConnect;

    private bool qrBool = false;
    public Button walletConnectBtn;
    public Button approveBtn;
    public Button stakeBtn;
    public Button soloBtn;
    public Button pvpBtn;
    public GameObject approveText;
    public GameObject walletMenu;
    public GameObject modeMenu;
    public GameObject stakeMenu;
    public TMP_InputField ammountInput;
    private Button QRCodeBtn;
    void Awake()
    {    
        var metadata = new ClientMeta()
        {
            Description = "Virtual Reality No-Loss Rythmic Game",
            Icons = new[] { "https://raw.githubusercontent.com/OpenDive/ApolloSaber_Dapp/main/client/publilc/opendive-circle.png" },
            Name = "Apollo Saber by OpenDive Technologies",
            URL = "http://apollo.opendive.io/"

            //Description = "WalletConnect Developer App",
            //Icons = new[] { "https://walletconnect.org/walletconnect-logo.png" },
            //Name = "WalletConnect",
            //URL = "https://walletconnect.org"
        };

        walletConnect = new WalletConnect(metadata);
        //Console.WriteLine(walletConnect.URI);
        uri = walletConnect.URI;
        Debug.Log(uri);
        //Connect();
    }

    void Start()
    {
        Button btnWallet = walletConnectBtn.GetComponent<Button>();
        btnWallet.onClick.AddListener(Connect);

        Button btnApprove = approveBtn.GetComponent<Button>();
        btnApprove.onClick.AddListener(ApproveBtnClicked);

        Button btnStake = stakeBtn.GetComponent<Button>();
        btnStake.onClick.AddListener(StakeBtnClicked);

        Button btnSolo = soloBtn.GetComponent<Button>();
        btnSolo.onClick.AddListener(SOLOBtnClicked);

        Button btnPvp = pvpBtn.GetComponent<Button>();
        btnPvp.onClick.AddListener(TournamentBtnClicked);
    }

    void Update()
    {

    }

    public async void Connect()
    {
        qrBool = !qrBool;
        walletMenu.SetActive(false);

        try
        {
            Debug.Log("Connecting using WalletConnect");
            Debug.Log("Waiting...");
            var walletConnectData = await walletConnect.Connect();
            Debug.Log("Account 0: " + walletConnectData.accounts[0]);
            Debug.Log("ChainId: " + walletConnectData.chainId);

            stakeMenu.SetActive(true);
            qrBool = false;
        }
        catch
        {
            Debug.Log("An error occurred");
        }
    }

    private void OnGUI()
    {
        if (qrBool)
        {
            Texture2D myQR = generateQR(uri);
            if (GUI.Button(new Rect(832, 412, 256, 256), myQR, GUIStyle.none)) 
            {
                //modeMenu.SetActive(true);
                //qrBool = false;
            }
        }
        
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public void SOLOBtnClicked()
    {
        Debug.Log("go to next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TournamentBtnClicked()
    {
        Debug.Log("go to next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void WalletConnectClicked()
    {
        qrBool = !qrBool;
        walletMenu.SetActive(false);
    }

    public void ApproveBtnClicked()
    {
        int amount = Int32.Parse(ammountInput.text);
        Debug.Log(amount);
        // Nethereum Async call APPROVE
        // Show dialog box: "BUSD Approve Succesful"
        approveText.SetActive(true);
    }

    public void StakeBtnClicked()
    {
        // Nethereum Async call STAKE
        // Show dialog box: "BUSD Stake Succesful"
        stakeMenu.SetActive(false);
        modeMenu.SetActive(true);
    }
}
