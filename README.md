# Home Center

**Home Center** היא מערכת חנות דסקטופ מבוססת **WPF** לניהול מוצרים, לקוחות, הזמנות, מועדון לקוחות ומשוב — עם ממשק נפרד ללקוח ולמנהל.

---

## דרישות מערכת

| דרישה | גרסה |
|--------|------|
| **Visual Studio** | 2017 ומעלה (מומלץ 2022) |
| **.NET Framework** | **4.7.2** |
| **SQL Server LocalDB** | `(LocalDB)\MSSQLLocalDB` |
| **Windows** | WPF — Desktop בלבד |

---

## מבנה הפרויקט

```
home_center/
├── home_center.sln          ← פתרון ראשי — אפליקציית החנות
├── home_center/
│   ├── App.xaml / App.xaml.cs
│   ├── MainWindow.xaml      ← חלון ראשי עם Frame לניווט
│   ├── MyPicture/           ← תמונות מוצרים (קבצים + העלאה)
│   ├── MyPicture.cs         ← טעינה/העלאת תמונות
│   ├── UI/                  ← דפי WPF (Page)
│   ├── BL/                  ← שכבת לוגיקה עסקית
│   ├── model/               ← Entity Framework (Database First)
│   └── Connected Services/  ← ServiceReference1 + ServiceReference2 (WCF)
└── Messege/                 ← מערכת הודעות — פתרון נפרד
    ├── Messege.sln
    ├── Messege/             ← שירות WCF + meddegedb.mdf
    ├── HOST/                ← אירוח שירות WCF
    └── MessageManager/      ← אפליקציית שליחת הודעות (MessageManager.sln)
```

> **שים לב:** `Messege/` **לא** כלול ב-`home_center.sln` — יש להריץ כפתרון עצמאי.

---

## ארכיטקטורה

```
┌─────────────────────────────────────────────────┐
│  MainWindow  →  Frame  →  Page (UI/)            │
│       ↓              ↓                          │
│   AllStatic     BL/*Service  →  Globali.myDb    │
│   (מצב סשן)          ↓                          │
│                 Entities4 (EF6)                 │
│                      ↓                          │
│              Databace.mdf (LocalDB)             │
└─────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────┐
│  message / MessageManager  →  WCF Service1      │
│                      ↓                          │
│              meddegedb.mdf (LocalDB נפרד)       │
└─────────────────────────────────────────────────┘
```

- **ניווט:** דפי `Page` בתוך `Frame` (`MainWindow` → `MainPage` / `director`).
- **גישה ל-DB:** **`Globali.myDb`** — singleton של **`Entities4`**.
- **מצב סשן:** **`AllStatic.myClient`** (לקוח מחובר) + **`AllStatic.myOrder`** (הזמנה/סל פעיל).

---

## חשיבה מקצה לקצה

### 1. קנייה מלאה — **לקוח**

| שלב | רכיב | מה קורה |
|-----|------|---------|
| **1** | `MainWindow` | טוען **`MainPage`** ב-Frame |
| **2** | `MainPage` → `pagebeforecast` | לקוח מזין **ת.ז.** → **`IdValidation.ValidateId`** |
| **3** | `ClientService.GetClient` | **קיים** → **`AllStatic.myClient`** + **`AllStatic.myOrder.ItemOrder.Clear()`** |
| **3א** | `addclient2` | **לא קיים** → השלמת רישום מהיר |
| **4** | `Pageofitem` | **`AllStatic.myOrder.Client = client`** → כפתורי קטגוריה → **`UserControl1`** לכל מוצר |
| **5** | `UserControl1` | בחירת כמות → **"הוסף לסל"** → **`AllStatic.myOrder.ItemOrder.Add(...)`** |
| **6** | `cart` | **`UserControl2`** לכל שורה → כפתור תשלום |
| **6א** | `Club_card` | אם **לא** חבר מועדון — הצעה להצטרפות → **`Member = true`** |
| **7** | `payment` | חישוב הנחות → אימות כרטיס → **`OrderService.AddOrder`** → **`UpdateAmount`** (מלאי) → איפוס סשן |

**הנחות מועדון ב-`payment`:**
- **`Member = true`** → **5%** הנחה
- **חודש יום הולדת** → **30%** הנחה נוספת
- **סכום < 200 ₪** → אין אפשרות תשלומים

---

### 2. הרשמת לקוח חדש — **מקצה לקצה**

| שלב | רכיב | מה קורה |
|-----|------|---------|
| **1** | `addclient` / `addclient2` | מילוי טופס + **`AgeRangeRule`** / **`isHebrewRule2`** / **`IsNumber`** |
| **2** | `IdValidation.ValidateId` | אימות ת.ז. ישראלית (9 ספרות, Luhn) |
| **3** | `ClientService.AddClient` | **`Status = true`** → שמירה ב-**`Databace.mdf`** |
| **4** | `MainPage` | חזרה לתפריט ראשי |

---

### 3. הצטרפות למועדון — **מקצה לקצה**

| שלב | רכיב | מה קורה |
|-----|------|---------|
| **1** | `MainPage` → `Club_card` / `cart` | כניסה דרך תפריט או מהסל |
| **2** | `Club_card` | זיהוי לפי ת.ז. → **`AllStatic.myClient`** |
| **3** | `ClientService.UpdateClient` | **`Member = true`** → שמירה |
| **4** | `payment` | הנחה מופעלת אוטומטית בקנייה הבאה |

---

### 4. תשלום ושמירת הזמנה — **מקצה לקצה**

| שלב | רכיב | מה קורה |
|-----|------|---------|
| **1** | `payment` | סכום = **`Sum(Amount × Price_base)`** |
| **2** | `IdValidation.IsValidIsraeliCreditCardNumber` | כרטיס **`53`** + 16 ספרות + Luhn |
| **3** | `OrderService.GetCode` | קוד הזמנה חדש → **`AllStatic.myOrder.CodeOrder`** |
| **4** | `OrderService.AddOrder` | שמירת **`Order`** + **`ItemOrder`** ב-DB |
| **5** | `ClientService.UpdateClient` | עדכון כתובת (**`Stree`**, **`HouseNumber`**, **`City`**) |
| **6** | `UpdateAmount` | **`Item.Amount -= Amount`** לכל פריט |
| **7** | איפוס | **`AllStatic.myOrder = new Order()`**, **`AllStatic.myClient = new Client()`** |

---

### 5. ניהול מנהל — **מקצה לקצה**

| שלב | רכיב | מה קורה |
|-----|------|---------|
| **1** | `MainPage` → `DirectorCode` | הזנת קוד → **`codedirectionService.Getcodedirector()`** |
| **2** | `director` | לוח בקרה עם Frame פנימי |
| **3** | `Kindofitem` / `Additem` | CRUD מוצרים + **`MyPicture.UploadImage_Dlg`** |
| **4** | `KindofCatgory` / `AddCatgory` | CRUD קטגוריות |
| **5** | `OrderClient` / `ItemOrder1` | צפייה בהזמנות ושורות |
| **6** | `ClientOfDirector` | ניהול לקוחות |
| **7** | `message` | צפייה בכל ההודעות מ-**WCF** |

**קוד מנהל ברירת מחדל:** **`25`** (טבלת **`codedirector`**, שדה **`code`**).

---

### 6. מערכת הודעות — **מקצה לקצה**

| שלב | רכיב | מה קורה |
|-----|------|---------|
| **1** | **`HOST`** (Messege.sln) | מפעיל WCF על **`localhost:8733`** |
| **2** | **`MessageManager`** | לקוח שולח הודעה → **`Service1.AddMessage`** → **`meddegedb.mdf`** |
| **3** | **`message`** (home_center) | מנהל/מערכת קורא **`GetAllMesseges`** → DataGrid |

**Endpoint:**
```
http://localhost:8733/Design_Time_Addresses/Messege/Service1/
```

---

## מסד נתונים ראשי

**Connection string פעיל:** **`Entities4`** ב-`App.config`

```
attachdbfilename=|DataDirectory|\Databace.mdf
data source=(LocalDB)\MSSQLLocalDB
```

### ישויות

| ישות | שדות עיקריים |
|------|-------------|
| **Category** | `Code`, `Dedripition`, `Status` |
| **Item** | `NameItem`, `Price_base`, `Price_after`, `Amount`, `NameImage`, `Category` |
| **Client** | `Id` (ת.ז.), `FirstName`, `LastName`, `Phone`, `Email`, `Stree`, `City`, `Member`, `Dateofbirth`, `Status` |
| **City** | `codeCity`, `NameCity` |
| **Order** | `CodeOrder`, `Id`, `DateOrder`, `ArriveDate`, `PriceEnd`, `Delivery`, `Payment` |
| **ItemOrder** | `Code`, `CodeItem`, `CodeOrders`, `Amount` |
| **codedirector** | `code`, `password` |

**DbContext:** **`home_center.model.Entities4`** ← **`Model1.edmx`** (Database First, EF6).

---

## שכבת BL

| שירות | תפקיד |
|--------|--------|
| **`Globali`** | **`Entities4 myDb`** — singleton |
| **`ClientService`** | Add / Get (פעילים) / Update |
| **`ItemService`** | CRUD + **`GetCode()`** |
| **`CategoryService`** | CRUD קטגוריות |
| **`OrderService`** | CRUD + **`GetCode()`** |
| **`ItemOrderService`** | שורות הזמנה + **`GetCode()`** |
| **`CityServise`** | רשימת ערים |
| **`codedirectionService`** | קוד מנהל |

---

## דפי UI

### לקוח (מחוברים לתפריט `MainPage`)

| דף | תפקיד |
|----|--------|
| **`MainPage`** | תפריט: מנהל, אודות, מוצרים, סל, עדכון, מועדון |
| **`pagebeforecast`** | הזדהות לפני קנייה |
| **`Pageofitem`** | קטלוג + סינון קטגוריה |
| **`cart`** | סל — **`UserControl2`** |
| **`payment`** | תשלום + משלוח |
| **`Club_card`** | הצטרפות למועדון |
| **`addclient` / `addclient2`** | הרשמה / עדכון |
| **`About`** | אודות |

### מנהל (מ-`director`)

| דף | תפקיד |
|----|--------|
| **`DirectorCode`** | אימות קוד |
| **`director`** | לוח בקרה |
| **`Kindofitem` / `Additem`** | מוצרים + תמונות |
| **`KindofCatgory` / `AddCatgory`** | קטגוריות |
| **`OrderClient` / `ItemOrder1`** | הזמנות |
| **`ClientOfDirector`** | לקוחות |
| **`message`** | הודעות (WCF) |
| **`ChageCode`** | שינוי קוד מנהל |

### קיים בקוד — **לא מחובר לתפריט**

| דף | מצב |
|----|-----|
| **`MessageClient`** | קיים; **`sendMessage`** מבוטל; אין ניווט מ-`MainPage` |
| **`Member`** | קיים; אין **`Navigate`** אליו בשום מקום |
| **`MainPage.opinion`** | handler קיים; **אין** MenuItem ב-XAML |

---

## תלויות (NuGet)

| חבילה | שימוש |
|--------|--------|
| **EntityFramework 6.2.0** | ORM |
| **HandyControls 3.5.3** | רכיבי UI |
| **System.Reactive 6.0.1** | Reactive Extensions |
| **Syncfusion.Xamarin.SfRating** | דירוג |
| **Xamarin.Forms 5.0.0** | תלות Syncfusion |

---

## הפעלה

### אפליקציה ראשית

1. **פתח** `home_center.sln`.
2. **Restore NuGet packages**.
3. **הכן** `Databace.mdf` ב-`bin\Debug` (ראה סעיף "מה חסר").
4. **הגדר** `home_center` כ-StartUp → **F5** → **`MainPage`**.

### מערכת הודעות

1. **פתח** `Messege/Messege.sln`.
2. **הרץ** **`HOST`** → שירות על **`8733`**.
3. **הרץ** `MessageManager/MessageManager.sln` לשליחת הודעות.

---

## מה חסר / לא עקבי בפרויקט

> מידע לצוות פיתוח — **מצב הקוד כפי שהוא**, לא רשימת שדרוגים.

### קבצים שלא ב-repo

| קובץ | השפעה |
|------|--------|
| **`Databace.mdf`** | ב-`.gitignore` — **האפליקציה לא תעבוד** בלי ליצור/לצרף DB |
| **`meddegedb.mdf`** | מסד הודעות — **WCF לא יעבוד** בלי קובץ זה |
| **`Class1`** | **`payment.xaml.cs`** מצהיר על **`Class1 tashlum`** — **המחלקה לא קיימת** ב-repo; עלול לחסום Build |

### אי-עקביות במצב סשן

- **`MainPage.Ouritem`** בודק **`AllStatic.myOrder.Client != null`**
- **`pagebeforecast`** מגדיר רק **`AllStatic.myClient`**
- **`Pageofitem`** מגדיר **`AllStatic.myOrder.Client`**
- **תוצאה:** חזרה ל"מוצרים" אחרי זיהוי לקוח עלולה **לדלג** על `pagebeforecast` גם בלי **`myOrder.Client`** מוגדר — תלוי בסדר הניווט

### תשלום (`payment`)

- **`AllStatic.myOrder.Id`** — **לא מוגדר** לפני **`AddOrder`** (FK ל-`Client` עלול להיות ריק)
- **`UpdateAmount()`** נקרא **אחרי** **`AllStatic.myOrder = new Order()`** — עדכון מלאי **עלול לא לרוץ** על ההזמנה הנכונה
- **`AddOrder`** נקרא **לפני** מילוי **`DateOrder`** / **`PriceEnd`** הסופיים — נשמרים רק ב-**`UpdateOrder`** שני

### דפים / תפריטים

- **`ChageCode`**: handlers **`again`** / **`againe1`** **ריקים** — שינוי קוד **לא נשמר**; **`Canchcode`** קיים ב-code-behind אך **אין** MenuItem ב-`Director.xaml`
- **`MessageClient`**: שליחה **מבוטלת** (`//service1.AddMessage`); שליחה אמיתית רק דרך **`MessageManager`**
- **`MainPage`**: **`opinion`** → `message` — **לא מקושר** לתפריט XAML
- **`Member`**: דף ריק — **לא בשימוש**

### תשתית

- **`App.config`**: **17+ connection strings** — רק **`Entities4`** בשימוש; השאר שאריות נתיבים ישנים
- **`ServiceReference1`** + **`ServiceReference2`**: **אותו endpoint** — כפילות
- **`MyPicture.GetImage`**: נתיב יחסי ל-**`bin\Debug`** — תלוי **Working Directory** בהרצה
- **שמות:** `Databace`, `Messege`, `Dedripition`, `Stree`, `CityServise` — שגיאות כתיב בכל הפרויקט (עקביות פנימית)

---

## טכנולוגיות

- **WPF** — `Page` + `Frame`, RTL
- **Entity Framework 6** — Database First
- **SQL Server LocalDB** — `.mdf`
- **WCF** — `BasicHttpBinding`
- **HandyControl** — UI

---

**Home Center** — חנות WPF עם זרימות **קנייה**, **מועדון**, **ניהול מנהל** ו**הודעות WCF**; מצב הסשן מרוכז ב-**`AllStatic`**, הנתונים ב-**`Globali.myDb`**, והממשק מחולק ל-**`MainPage`** (לקוח) ו-**`director`** (מנהל).
