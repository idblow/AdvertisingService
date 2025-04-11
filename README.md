# AdvertisingService
## Описание проекта
Веб-сервис для подбора рекламных площадок по географическим локациям. Сервис позволяет загружать данные о рекламных площадках и их локациях, быстро находить все площадки, действующие в указанной локации, поддерживает вложенность локаций (регион → город → район)

## Технологии
* .NET 6
* ASP.NET Core Web API
* Swagger (для документации API)

## Требования
.NET 6 SDK 

## Установка и запуск
1. Клонируйте репозиторий <br>
`git clone https://github.com/idblow/AdvertisingService` <br>
`cd AdvertisingPlatformService`
2. Восстановите зависимости и запустите сервер <br>
`dotnet restore`<br>
`dotnet run --project AdvertisingService`

## Использование API
1. Загрузка данных о площадках
<pre>
POST /advertising/load
Content-Type: application/json
{
  "filePath": "путь/к/файлу.txt"
}
</pre>

Пример файла данных (platforms.txt):<br>
<pre>
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
</pre>

2. Поиск площадок по локации
<pre>
GET /advertising/platforms?location=/ru/msk
Accept: application/json
</pre>
Пример ответа:<br>
`["Яндекс.Директ", "Газета уральских москвичей"]`

## Структура проекта
<pre>
AdvertisingService/
├── Controllers/           # API контроллеры
│   └── AdvertisingController.cs
├── Services/         # Бизнес-логика
│       └── AdvertisingServices.cs
├── Models/               # Модели данных
│   └── FileUploadRequest.cs
├── Properties/
│   └── launchSettings.json
├── appsettings.json      # Конфигурация
└── Program.cs            # Точка входа
</pre>
