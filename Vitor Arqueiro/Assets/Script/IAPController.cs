using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPController : MonoBehaviour, IStoreListener
{
    private Player player; 
    public static IAPController instancia;
    public static IStoreController iStoreController;
    public static IExtensionProvider iExtensionProvider;
    public static string produto2000Moedas = "2000moedas";
    public static string produto10000Moedas = "10000moedas";
    public string preco2000Moedas;
    public string preco10000Moedas;
    void Awake() {
        if(instancia == null){
            instancia = this;
            DontDestroyOnLoad(instancia);
        } else {
            Destroy(instancia);
        }
    }
    public void  IniciarVenda() {
        if( LojaInicializada() ) {
            return;
        }

        StandardPurchasingModule moduloCompra = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(moduloCompra);

        builder.AddProduct(produto2000Moedas, ProductType.Consumable);
        builder.AddProduct(produto10000Moedas, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }
    private bool LojaInicializada() {
        return iStoreController != null && iExtensionProvider != null;
    }
    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {   
        Debug.Log("Loja inicializada com sucesso");
        iStoreController = controller;
        iExtensionProvider = extensions;
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("A Não Loja não foi inicializada com sucesso "+error);
    }

    void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        player = GameObject.FindObjectOfType<Player>();
        if(player !=null) {
            player.addCoinsQtd(2000);
            Debug.Log("Compra FInalizada, item adiciona na conta do usuario");
        } else {
            Debug.Log("Compra FInalizada, ERRO AO ADICIONAR ITEM PLAYER NULL");
        }
        return PurchaseProcessingResult.Complete;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!LojaInicializada()) {
            IniciarVenda();
            inicializaPrecos();
        }
    }
    public void inicializaPrecos() {
        preco2000Moedas = iStoreController.products.WithID(produto2000Moedas).metadata.localizedPriceString;
        preco10000Moedas = iStoreController.products.WithID(produto10000Moedas).metadata.localizedPriceString;
    }

    public bool CompraProduto(string codigoProduto){
        if(LojaInicializada()){
            Product produto = iStoreController.products.WithID(codigoProduto);
            if(produto != null && produto.availableToPurchase){
                iStoreController.InitiatePurchase(codigoProduto);
                Debug.Log("Processo Compra Realizada Com sucesso Produto: "+produto.definition.id+ " -preço: "+produto.metadata.localizedPriceString);
                return true;
            } else {
                Debug.Log("Erro na Compra do Produto: "+ codigoProduto + " Produto não encontrado ");
            }
        } else {
            Debug.Log("Erro na Compra do Produto: "+ codigoProduto + " Produto não encontrado ");
        }
        return false;
    }

}
