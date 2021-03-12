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

The service ALWAYS returns 200.

API for admin
--

Url: /admin

Method: POST - create an endpoint

Body: n/a

Possible responses:

Code: 400
Body:

    {
        "StatusCode":403,
        "Message":"WebhookDebugger.Domain.Exceptions.OutOfHosts"
    }

Code: 200

Body: 

    {
        "identification": "[Guid that can be used for test]"
    }


Url: /admin/{identification}

Method: GET

Body: n/a

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


API for test
--

Url: /

Method: GET/PUT/POST/DELETE/OPTIONS

Body: [as you see fit]

Possible responses:

Code: 200

Body: n/a
