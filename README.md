# SeleniumConsoleApp

## 相關參數說明

```csharp

// 測試目標網址
private readonly static string _url = "";

// Chrome Driver 位置 (看本機Chrome版本判斷要抓哪個Driver到根目錄)
var driver = new ChromeDriver(".")
```

### parameter.json

```json
{
  //循環檢查時間間隔
  "Timer": 10, 
  //08點開始觸發時間 Ex: 08:30 判斷是否點擊
  "SignInAt": 30, 
  //08點40以後不檢查
  "SignInBefore": 40, 
  //18點開始觸發時間 Ex: 18:35 判斷是否點擊
  "SignOutAt": 35, 
  //18點45以後不檢查
  "SignOutBefore": 45, 
  //每週哪幾天要執行
  "DayOfWeek": [
    1,
    2,
    3,
    4,
    5
  ], 
  //生日問題答案
  "Birthday": "", 
  //到職日問題答案
  "JobDay": "",
  //電子信箱問題答案
  "EMail": "", 
  //身分證字號問題答案
  "Id": "" 
}
```
