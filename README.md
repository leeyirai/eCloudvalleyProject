# eCloudvalleyProject
interview Project

## 簡介
本專案使用.NET Web API，採用三層式架構開發，功能主要用於取得AWS_Billing相關資訊。  
資料庫選用Sqlite。


## 相關資訊
* **環境資訊**
1.  開發環境使用VS 20221編譯器、資料庫則使用DB_Browser_for_SQLite。
1.   將專案下載至本地端後，需將SqliteBrowser資料夾內(.rar)檔案解壓縮並置於相同資料夾裡  (路徑應為: ...\eCloudvalleyProject\SqliteBrowser\eCloudvalley.db)，該檔案為測試用的Low Data。
1.  完成上述步驟後即可啟動專案進行開發與測試。

## 說明
### API (均以本地端localhost啟動專案之環境說明)
* 第一支服務: Get lineItem/UnblendedCost grouping by product/productname：  
  透過傳入lineitem/usageaccountid可取得每個product/productname的lineItem/UnblendedCost總和。

HTTPGET，  
URL: https://localhost:7128/api/Billing/GetUnblendedCost?usageAccountId=輸入欲查詢usageAccountId  
e.g. https://localhost:7128/api/Billing/GetUnblendedCost?usageAccountId=610810069647

傳入

| 欄位名稱       | 屬性   | 必要 | 說明                                                                                                                                                                                     |
| -------------- | ------ | ---- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| usageAccountId | string | Y    | The ID of the account that used this line item. For organizations, this can be either the master account or a member account. You can use this field to track costs or usage by account. |


傳出

```
    {
        "{product/productname_A}": "sum(lineitem/unblendedcost)",
        "{product/productname_B}": "sum(lineitem/unblendedcost)",
        ...
    }
```


* 第二支服務: Get daily lineItem/UsageAmount grouping by product/productname：  
  透過傳入lineitem/usageaccountid可取得每個product/productname每日的lineItem/UsageAmount總和。  

HTTPGET，  
URL: https://localhost:7128/api/Billing/GetUsageAmount?usageAccountId=輸入欲查詢usageAccountId  
e.g. https://localhost:7128/api/Billing/GetUsageAmount?usageAccountId=610810069647

傳入

| 欄位名稱       | 屬性   | 必要 | 說明                                                                                                                                                                                     |
| -------------- | ------ | ---- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| usageAccountId | string | Y    | The ID of the account that used this line item. For organizations, this can be either the master account or a member account. You can use this field to track costs or usage by account. |

傳出

```
    {
        "{product/productname_A}": {
            "YYYY/MM/01": "sum(lineItem/UsageAmount)",
            "YYYY/MM/02": "sum(lineItem/UsageAmount)",
            "YYYY/MM/03": "sum(lineItem/UsageAmount)",
            ...
        },
        "{product/productname_B}": {
            "YYYY/MM/01": "sum(lineItem/UsageAmount)",
            "YYYY/MM/02": "sum(lineItem/UsageAmount)",
            "YYYY/MM/03": "sum(lineItem/UsageAmount)",
            ...
        },
    }
```

另外，也可透過測試環境的Swagger文件了解更多資訊。(https://localhost:7128/swagger/index.html)

### DB (Sqlite)


| 欄位名稱 | 屬性 | 說明 |
| -------- | ---- | ---- |
|    pkno*      |   INTEGER   |   唯一值   |
|     bill_payerAccountId     |   INTEGER   |  The account ID of the paying account. For an organization in AWS Organizations, this is the account ID of the master account.    |
|      lineItem_unblendedCost    |   REAL   |   The UnblendedCost is the UnblendedRate multiplied by the UsageAmount.   |
|     lineItem_unblendedRate     |   Text   |    The uncombined rate for specific usage. For line items that have an RI discount applied to them, the UnblendedRate is zero. Line items with an RI discount have a UsageType of Discounted Usage.  |
|    lineItem_usageAccountId      |  INTEGER    |  The ID of the account that used this line item. For organizations, this can be either the master account or a member account. You can use this field to track costs or usage by account.    |
|     lineItem_usageAmount     |    REAL  |    The amount of usage that you incurred during the specified time period. For size-flexible reserved instances, use the reservation/TotalReservedUnits column instead.  |
|    lineItem_usageStartDate      |   Text   |    The start date and time for the line item in UTC, inclusive. The format is YYYY-MM-DDTHH:mm:ssZ.  |
|     lineItem_usageEndDate     |    Text  |   The end date and time for the corresponding line item in UTC, exclusive. The format is YYYY-MM-DDTHH:mm:ssZ.   |
| product_productName     | Text | The full name of the AWS service. Use this column to filter AWS usage by AWS service. Sample values: AWS Backup, AWS Config, Amazon Registrar, Amazon Elastic File System, Amazon Elastic Compute Cloud |

* 將pkno設為PRIMARY KEY，用於快速識別並查詢資料。
* 根據已開發服務內容，將lineItem_usageAccountId設為非叢集索引縮短查詢時間。
