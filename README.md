# Demo ZaloPay SDK Xamarin Forms
Alpha version of demo

## Configure your app id
- First thing first, you need to configure your app_id and other configs in `Constants.cs`
- Implement a API to replace Sqlite then replacing ZaloPay URL by your API URL
- In correct case, this app have to call your API and then it will call ZaloPay API

## Prerequisties
- Visual Studio 2019 or above


## Description
- This is a crafted xamarin demo of intergration with ZaloPay.
- Data store in local strorage by Sqlite, so merchant need to implement 
and API to handle storing persitent data and also the scheduled job for
updating transaction status

## Licence
 Copyright 2011 Xamarin Inc

 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.