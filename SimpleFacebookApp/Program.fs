open System
open System.Linq
open System.Collections
open System.Collections.Generic
open System.Configuration
open Facebook

// Function helps cast object, equivalent of 'as'
let castAs<'T when 'T : null> (o:obj) = 
  match o with
  | :? 'T as res -> Some res
  | _ -> None


// Function casts dynamic object to dictionary
// and it prints out all values
let print_properties dObject =
    match castAs<IDictionary<string, System.Object>>(dObject) with
    | Some dict ->
        dict.Keys
        |> Seq.iter(fun i->
            printfn "%A %A" i dict.[i]
        )
    | _ ->
        printfn "I cannot recognize object %A" dObject
    
[<EntryPoint>]
let main argv = 
    
    printfn "Using facebook API anonymously"
    let client = new FacebookClient()
    // getting information about Zuck (I believe you know who he is)
    let zuck = client.Get("zuck")
    print_properties zuck
    printfn ""

    printfn "Using facebook API with user access token"
    let accessToken = ConfigurationManager.AppSettings.["appUserToken"]
    let user_client = new FacebookClient(accessToken)
    // getting information about user himself by using keywork 'me'
    let user = user_client.Get("me")
    print_properties user

    Console.ReadLine() |> ignore

    0 