Webhook Debugger
===

Purpose
--

This API is used to see what is send to a webhook in testing environments.

There is no UI, only a REST API. Via the admin API you can create a custom endpoint and query the last 4 actions send to this endpoint.

Possible HTTP methods to use on the given endpoint

* GET
* OPTIONS
* PUT
* POST
* DELETE
* HEAD

The service ALWAYS returns 200.

Usage scenario
--

1. Create endpoint via the admin API


    curl --location 'https://localhost:58212/admin' --header 'Content-Type: application/json' 


2. POST response:

    
    {
      "identification": "8f8c1e2e-6f4b-4d3a-9f7e-2c3b5e6d7f8"
    }


3. Use the endpoint in your application as webhook (Here GET)


    curl --location 'https://localhost:58212/8f8c1e2e-6f4b-4d3a-9f7e-2c3b5e6d7f8'

4. Response


    200 OK


5. Query the admin API to see what has happened

   
    curl --location 'https://localhost:58212/admin/edf46197-ef6f-45ec-aba8-b3d84537d943'


6. Response


    [
        {
            "queryString": "",
            "contentLength": 0,
            "url": "/73c74e33-82c6-411b-a3c9-6cd925a82b17",
            "body": null,
            "headers": [
                { "name": "Accept", "value": "*/*" },
                { "name": "Connection", "value": "keep-alive" },
                { "name": "Host", "value": "localhost:58212" },
                { "name": "User-Agent", "value": "PostmanRuntime/7.48.0" },
                { "name": "Accept-Encoding", "value": "gzip, deflate, br" },
                { "name": "Cache-Control", "value": "no-cache" },
                { "name": "Postman-Token", "value": "17a841b5-dc78-4c35-a69d-cc91dc741143" }
            ],
            "method": "GET",
            "received": "2025-10-08T13:44:18.2990721+02:00"
        }
    ]


API for admin
--

Url: /admin

### Method: POST - create an endpoint


Body: n/a

Possible responses:

__Code: 400__
Body:

    {
        "StatusCode":403,
        "Message":"WebhookDebugger.Domain.Exceptions.OutOfHosts"
    }

__Code: 200__

Body: 

    {
        "identification": "[Guid that can be used for test]"
    }


Url: /admin/{identification}

### Method: GET

Possible responses:

Code: 400

Body:

    {
           "StatusCode":400,
           "Message":"WebhookDebugger.Domain.Exceptions.OutOfHosts"
    }

Code: 200
Body: 

	[
        {
            "queryString": "[Query string]",
            "url": "[The url used, inclding query parameters]",
            "body": [Valu of body, is any send],
            "headers": [
                {
                    "name": "[Name of header",
                    "value": "[Value of header"
                }
            ],
            "method": "[used method]",
            "received": "[date time]"
        }
    ]


API for debugging
--

Url: /[identification]

Method: GET/PUT/POST/DELETE/OPTIONS/HEAD

Body: [as you see fit]

Possible responses:

__Code: 200__

Body: n/a


Postman collections
--

Active environment must have one parameter:


    https-host = https://localhost:58212/ (Or whatever endpoint you se it to)

When the POST to /ADMIN returns, the returned identification is copied to the environment variable:

    endpoint-guid = [identification returned from ADMIN/POST]


Postman collections for ADMIN API can be found in the Postman folder:

    .\Postman\Admin.postman_collection.json


Postman collection to test the webhook can be found in the Postman folder:

    .\Postman\WebhookDebugger.postman_collection.json

