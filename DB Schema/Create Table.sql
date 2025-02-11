USE CathayInterviewDB;

CREATE TABLE Currency (
    CurrencyId INT IDENTITY(1,1) PRIMARY KEY,
    CurrencyCode NVARCHAR(3) UNIQUE NOT NULL, -- ISO 4217 貨幣代碼（如 USD, EUR, JPY）
    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Currency_CreatedAt DEFAULT GETDATE()
);

-- 添加 Currency 表的註解
EXEC sp_addextendedproperty 'MS_Description', '存儲貨幣的基本資訊，如貨幣代碼', 
    'SCHEMA', 'dbo', 'TABLE', 'Currency';

-- 添加 Currency 欄位註解
EXEC sp_addextendedproperty 'MS_Description', '貨幣唯一識別碼（主鍵）', 
    'SCHEMA', 'dbo', 'TABLE', 'Currency', 'COLUMN', 'CurrencyId';

EXEC sp_addextendedproperty 'MS_Description', '貨幣代碼（例如 USD, EUR, JPY），符合 ISO 4217 標準', 
    'SCHEMA', 'dbo', 'TABLE', 'Currency', 'COLUMN', 'CurrencyCode';

EXEC sp_addextendedproperty 'MS_Description', '貨幣創建時間，預設為當前時間', 
    'SCHEMA', 'dbo', 'TABLE', 'Currency', 'COLUMN', 'CreatedAt';

-- 語言表
CREATE TABLE Language (
    LanguageId INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(5) UNIQUE NOT NULL, -- ISO 639-1 語言代碼（如 en, zh-TW, ja）
    DisplayName NVARCHAR(50) NOT NULL
);

-- 添加 Language 表的註解
EXEC sp_addextendedproperty 'MS_Description', '存儲系統支援的語言代碼及名稱', 
    'SCHEMA', 'dbo', 'TABLE', 'Language';

-- 添加 Language 欄位註解
EXEC sp_addextendedproperty 'MS_Description', '語言唯一識別碼（主鍵）', 
    'SCHEMA', 'dbo', 'TABLE', 'Language', 'COLUMN', 'LanguageId';

EXEC sp_addextendedproperty 'MS_Description', '語言代碼（例如 en, zh-TW, ja），符合 ISO 639 標準', 
    'SCHEMA', 'dbo', 'TABLE', 'Language', 'COLUMN', 'Code';

EXEC sp_addextendedproperty 'MS_Description', '語言的可讀名稱（例如 English、繁體中文、日本語）', 
    'SCHEMA', 'dbo', 'TABLE', 'Language', 'COLUMN', 'DisplayName';

-- 貨幣翻譯表（多語系支援）
CREATE TABLE CurrencyTranslation (
    CurrencyTranslationId INT IDENTITY(1,1) PRIMARY KEY,
    CurrencyId INT NOT NULL,
    LanguageId INT NOT NULL,
    CurrencyName NVARCHAR(100) NOT NULL CHECK (LEN(CurrencyName) > 0), -- 確保名稱不為空

    CONSTRAINT FK_CurrencyTranslation_Currency FOREIGN KEY (CurrencyId) 
    REFERENCES Currency(CurrencyId) ON DELETE CASCADE,

    CONSTRAINT FK_CurrencyTranslation_Language FOREIGN KEY (LanguageId) 
    REFERENCES Language(LanguageId) ON DELETE CASCADE,

    UNIQUE (CurrencyId, LanguageId)
);

-- 添加 CurrencyTranslation 表的註解
EXEC sp_addextendedproperty 'MS_Description', '存儲貨幣的多語系名稱（貨幣與語言的關聯）', 
    'SCHEMA', 'dbo', 'TABLE', 'CurrencyTranslation';

-- 添加 CurrencyTranslation 欄位註解
EXEC sp_addextendedproperty 'MS_Description', '貨幣翻譯的唯一識別碼（主鍵）', 
    'SCHEMA', 'dbo', 'TABLE', 'CurrencyTranslation', 'COLUMN', 'CurrencyTranslationId';

EXEC sp_addextendedproperty 'MS_Description', '關聯的貨幣 ID（對應 Currency.CurrencyId）', 
    'SCHEMA', 'dbo', 'TABLE', 'CurrencyTranslation', 'COLUMN', 'CurrencyId';

EXEC sp_addextendedproperty 'MS_Description', '關聯的語言 ID（對應 Language.LanguageId）', 
    'SCHEMA', 'dbo', 'TABLE', 'CurrencyTranslation', 'COLUMN', 'LanguageId';

EXEC sp_addextendedproperty 'MS_Description', '該語言對應的貨幣名稱', 
    'SCHEMA', 'dbo', 'TABLE', 'CurrencyTranslation', 'COLUMN', 'CurrencyName';
