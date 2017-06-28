/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class IAP : IStoreListener
{
    public static IStoreController m_StoreController;          // The Unity Purchasing system.
	public static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    const string PACKAGE_NAME = "com.ralpgames.kingdomsorceress";

    public const string PRODUCT_ID_REMOVE_ADS = PACKAGE_NAME + ".removeads";
    public const string PRODUCT_ID_BUY_COINS1 = PACKAGE_NAME + ".buycoins1";
    public const string PRODUCT_ID_BUY_COINS2 = PACKAGE_NAME + ".buycoins2";
    public const string PRODUCT_ID_BUY_COINS3 = PACKAGE_NAME + ".buycoins3";
    public const string PRODUCT_ID_BUY_MAGNETS = PACKAGE_NAME + ".buymagnets";
    public const string PRODUCT_ID_BUY_HINTS = PACKAGE_NAME + ".buyhints";

    public IAP()
    {
        InitializePurchasing();
    }

    public void InitializePurchasing() 
    {
		// If we have already connected to Purchasing ...
		if (IsInitialized()) {
			// ... we are done here.
			return;
		}
		
		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		
		// Add a product to sell / restore by way of its identifier, associating the general identifier
		// with its store-specific identifiers.
		builder.AddProduct(PRODUCT_ID_REMOVE_ADS, ProductType.NonConsumable);
		builder.AddProduct(PRODUCT_ID_BUY_COINS1, ProductType.Consumable);
		builder.AddProduct(PRODUCT_ID_BUY_COINS2, ProductType.Consumable);
		builder.AddProduct(PRODUCT_ID_BUY_COINS3, ProductType.Consumable);
        builder.AddProduct(PRODUCT_ID_BUY_MAGNETS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_ID_BUY_HINTS, ProductType.Consumable);
		
		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}

    public static bool IsInitialized() 
    {
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

    public void BuyProductID(string productId) 
    {
        // If Purchasing has been initialized ...
        if (IsInitialized()) {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);
            
            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase) {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases() 
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized()) {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        
        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer || 
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");
            
            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions) 
    {
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");
		
		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
    {
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
		if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_ID_REMOVE_ADS, StringComparison.Ordinal)) // REMOVE ADS
        {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //Config.SetShowAds(false);
		}
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_ID_BUY_COINS1, StringComparison.Ordinal)) // BUY COINS
        {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //Stats.SetCoins(Stats.Coins() + 10000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_ID_BUY_COINS2, StringComparison.Ordinal)) // BUY COINS
        {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //Stats.SetCoins(Stats.Coins() + 25000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_ID_BUY_COINS3, StringComparison.Ordinal)) // BUY COINS
        {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //Stats.SetCoins(Stats.Coins() + 100000);
        }
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_ID_BUY_MAGNETS, StringComparison.Ordinal)) // BUY MAGNETS
        {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //Stats.SetMagnetHints(Stats.MagnetHints() + 10);
        }
		else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_ID_BUY_HINTS, StringComparison.Ordinal)) // BUY HINTS
        {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            //Stats.SetHints(Stats.Hints() + 15);
        }
		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
		else
        {
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

        UpdateStats();

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}

    public void UpdateStats()
    {
        //LobbyManager mgr = GameObject.Find("Manager").GetComponent<LobbyManager>();
        //mgr.hud.UpdateStats();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) 
    {
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

}
*/