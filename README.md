# Blazing.Twilio.Video

## Requirements

> .NET Core 3.1
> Visual Studio 2019

## To configure environment variables

Add environment variables to appsettings.Development.json:

> "TwilioAccountSid": "[ACCOUNT SID]",
> "TwilioApiSecret": "[API SECRET]",
> "TwilioApiKey": "[API keys are revokable credentials for the Twilio API]",

Or by running command lines:

> setx TWILIO_ACCOUNT_SID [ACCOUNT SID]
> setx TWILIO_API_SECRET [API Secret]
> setx TWILIO_API_KEY [SID]

To learn more - [Generate Twilio API Key](https://www.twilio.com/docs/iam/keys/api-key)


## To run the app

Build and run the application to ensure that it compiles and works properly: press F5 to do this from either Visual Studio or Visual Studio Code, or run the app from the .NET CLI with the dotnet run command:

```bash
dotnet build && dotnet run -p Server/Blazor.Twilio.Video.Server.csproj
```

## App start (user 1)
![app start](images/app-start.png)

## Camera selected (user 1)
![camera selected](images/camera-selected.png)

## Participant (user 2)
![another user](images/participant.png)

## Room created (user 1)
![room created](images/room-created.png)

## Join room (user 2)
![app start](images/party.png)

## Party ensues (user 1)
![app start](images/party-0.png)
