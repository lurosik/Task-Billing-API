# Billing API

## Introduction

Implementation is based on number of different assumptions. I will be happy to tell more about it during a talk/call and/or answer the questions or tell a bit more about "decision dirvers".

Main assumptions:

  - there is no authorization (API it will be used from local network). If there will be authorization required it can be added.
  - swagger is used for API testing/interaction/discovery
  - there is only 1 REST method implemented POST that returns 
    - 200 if everything is ok, 
    - 500 if processing has failed
    - 400 if the request is improper (with information what is wrong) or other error has occured
  - there is no circuit breaker, timeouts or anything like that implemented in case when a payment gateway is not available etc.
  - there is currently no logging added (it is not a problem to add NLog for logging purposes, but there will be some more information required what we shall/can/allowed to log and where/how)
  - E-Shop platform is treated as a system that will use the API and is treated as a "frontend" or quite simple backend
  - there is no storage of the received/processed orders i.e. in database or other storage (even flat files or cloud storage). It can be added if required
  - Receipt object is treated as a simple "acknowledgement" message with very simple data (if required it could be extended)
  - there is no support of multiple orders send with the same data (all of them will be processed - push further to appropriate payment gateway). It is possible to add this kind of "cache" for incoming order to drop next request with the same data for the given period of time (i.e. 15, 30, 60 seconds)
  - All fields have some types that are quite broad to support variety of applications/needs (they could be simplified if required)
  - typical scenario is:

        E-Shop --[order]--> Billing API --[Payment order]--> A Payment Gateway --+
                                                                                 |
                E-Shop <--[receipt]-- Billing API <--[Payment result/status]-----+
        
  - There is also an assumption that in *order* object there is a name of payment gateway we would like to use - there are implemented 2 dummy gateways for 2 fictive companies "Pay Fast Ltd." and "Fast Cash Ltd." that has its own names assigned ("PayFast" and "FastCash" appropriatelly). I assume that there could be more payment gateways and there could be some more payment gataways added in the future, for that purpuse it will be probably the best way to extend impelmentation to support some dynamic configuration and/or plugin-way loading.
  - There is no docker file, but it can be added if required.

## Libraries/Assemblies/Projects overview

Solution is written using .net core 3.1 (LTS), libraries are using .net standard 2.0.
Eshop Visual Studio solution consist following projects:

  - BillingApi - project with API and main controller called BillingController
  - BillingApiTests - test project that covers main classes/features of BillingApi project
  - GatewayLibrary - contains payment gateway related DTOs and interface(s)
  - PaymentGateways - contains dummy implementiation of 2 different gateways belonging to different payment vendors

  Because GatewayLibrary and PaymentGateways projects do not contain valuable business logic but are either simple data structures, behaviour definitions or "mocks" they are not unit tested.