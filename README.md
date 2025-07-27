# 帳戶管理系統

這是一個使用 ASP.NET Core MVC 建立的使用者帳戶管理系統，支援完整的 CRUD 操作功能。

## 功能特色

- ✨ **使用者管理**: 完整的新增、查看、編輯、刪除功能
- 🗃️ **SQLite 資料庫**: 輕量級資料庫，易於部署和開發
- 🎨 **響應式設計**: 使用 Bootstrap 5 建立現代化介面
- 📱 **行動裝置友善**: 支援各種螢幕尺寸
- 🔍 **資料驗證**: 前後端雙重驗證確保資料正確性
- 🌐 **中文介面**: 完全中文化的使用者介面

## 技術堆疊

- **後端框架**: ASP.NET Core MVC 8.0
- **資料庫**: SQLite
- **ORM**: Entity Framework Core
- **前端**: Bootstrap 5 + Font Awesome + jQuery
- **開發語言**: C#

## 系統需求

- .NET 8.0 SDK
- Visual Studio Code 或 Visual Studio 2022

## 快速開始

### 1. 下載專案
```bash
git clone https://github.com/ppasszzw311/accountaa.git
cd account-web
```

### 2. 安裝相依套件
```bash
dotnet restore
```

### 3. 建立資料庫
```bash
dotnet ef database update
```

### 4. 運行應用程式
```bash
dotnet run
```

**注意**: 如果在 http://localhost:5261 訪問時出現資料庫相關錯誤，請確保：
1. 資料庫檔案 `AccountApp.db` 存在於專案根目錄
2. 已執行 `dotnet ef database update` 來應用最新的遷移
3. 如果問題持續，可以刪除 `AccountApp.db` 檔案並重新執行遷移

### 5. 開啟瀏覽器
導航至 `https://localhost:7232` 或 `http://localhost:5261`

## 專案結構

```
account-web/
├── Controllers/        # MVC 控制器
│   ├── HomeController.cs
│   └── UsersController.cs
├── Data/              # 資料層
│   └── ApplicationDbContext.cs
├── Models/            # 資料模型
│   ├── BaseModel.cs
│   ├── User.cs
│   ├── Account.cs
│   ├── Factory.cs
│   ├── Group.cs
│   ├── Project.cs
│   ├── ProjectMember.cs
│   ├── UserGroupMapping.cs
│   ├── UserRoleMapping.cs
│   ├── LoginRecord.cs
│   ├── AccountActionRecord.cs
│   ├── Domain.cs
│   ├── Role.cs
│   ├── ErrorViewModel.cs
│   └── Dtos/
├── Views/             # Razor 視圖
│   ├── Home/
│   ├── Users/
│   └── Shared/
├── Migrations/        # EF 遷移檔案
├── wwwroot/          # 靜態檔案
└── Program.cs        # 應用程式進入點
```

## 使用者實體屬性

| 屬性 | 類型 | 必填 | 說明 |
|------|------|------|------|
| Id | int | ✓ | 主鍵，自動遞增 |
| UserId | string | ✓ | 使用者ID (最大 50 字元，唯一) |
| Password | string | ✓ | 使用者密碼 (最大 255 字元) |
| Name | string | ✓ | 使用者姓名 (最大 100 字元) |
| FactoryId | string | ✓ | 工廠ID (最大 20 字元) |
| CreatedAt | DateTime | ✓ | 建立時間 |
| UpdatedAt | DateTime | ✓ | 更新時間 |

## 功能說明

### 使用者管理功能
- **列表頁面**: 顯示所有使用者資料，支援查看、編輯、刪除操作
- **新增使用者**: 表單驗證與資料新增
- **編輯使用者**: 更新使用者資訊
- **檢視詳細**: 顯示使用者完整資訊
- **刪除確認**: 安全的刪除確認機制

### 資料驗證
- 使用者ID：必填，最大 50 字元，唯一性檢查
- 密碼：必填，最大 255 字元
- 姓名：必填，最大 100 字元
- 工廠ID：必填，最大 20 字元

## 開發說明

### 新增 Migration
```bash
dotnet ef migrations add [MigrationName]
dotnet ef database update
```

### 開發模式運行
```bash
dotnet watch run
```

### 建置專案
```bash
dotnet build
dotnet publish -c Release
```

### 疑難排解

#### 資料庫問題
如果遇到資料庫相關錯誤：
```bash
# 重置資料庫
rm AccountApp.db
dotnet ef database update

# 或者重新建立遷移
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### 端口問題
- HTTPS 預設端口：7232
- HTTP 預設端口：5261
- 可在 `Properties/launchSettings.json` 中修改端口設定

## 自訂化

您可以輕鬆擴展此專案：

1. **新增欄位**: 修改 `User` 模型並建立新的 migration
2. **自訂樣式**: 編輯 `wwwroot/css/site.css`
3. **新增控制器**: 建立新的控制器和對應的視圖
4. **資料庫變更**: 使用 Entity Framework migrations

## 授權

此專案使用 MIT 授權條款。

## 貢獻

歡迎提交 Issue 和 Pull Request 來改善此專案！

---

**開發者**: 帳戶管理系統團隊  
**版本**: 1.0.0  
**最後更新**: 2025年7月25日
