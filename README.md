[![](https://img.shields.io/nuget/v/soenneker.attio.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.attio.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.attio.openapiclient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.attio.openapiclient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.attio.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.attio.openapiclient/)

# Soenneker.Attio.OpenApiClient

A generated, strongly typed .NET client for the Attio v2 API. The client uses Kiota request builders and models generated from Attio's OpenAPI definitions.

## Installation

```bash
dotnet add package Soenneker.Attio.OpenApiClient
```

## Create a client

The generated package accepts any Kiota `IRequestAdapter`. This example uses an `HttpClient` with Attio bearer authentication:

```csharp
using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Attio.OpenApiClient;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", attioAccessToken);

var adapter = new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient);

var client = new AttioOpenApiClient(adapter);
```

The generated client sets the adapter base URL to `https://api.attio.com` when the adapter does not already have one.

## Call the API

Request builders follow the URL hierarchy. For example, inspect the current access token and workspace:

```csharp
var tokenInfo = await client.V2.Self.GetAsync(
    cancellationToken: cancellationToken);

if (tokenInfo?.Active == true)
    Console.WriteLine(tokenInfo.WorkspaceName);
```

The root `V2` request builder also exposes activities, comments, emails, files, lists, meetings, notes, objects, sequences, SQL, tasks, threads, webhooks, and workspace members.

Kiota request configuration can add headers and request options:

```csharp
var tokenInfo = await client.V2.Self.GetAsync(
    request => request.Headers.Add("X-Request-ID", requestId),
    cancellationToken);
```

## Generated-code considerations

- Operations return nullable response models when the API can return no body.
- API failures are surfaced by the Kiota request adapter; handle them at the application boundary appropriate for your use case.
- Generated type and property names follow the source OpenAPI document and can change when that document changes.
- Do not edit files under the generated client project expecting those edits to survive regeneration.

For an application-ready DI registration that supplies authentication and caches the generated client, use `Soenneker.Attio.OpenApiClientUtil`.
