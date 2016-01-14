#r "packages/FAKE/tools/fakelib.dll"

open Fake

[<AutoOpen>]
module Config =
    let prjName = "ConsulServices"
    let mainSln = prjName + ".sln"
    let mainPrj = prjName
    
    let outputFolder = "output"

    let buildMode () = getBuildParamOrDefault "buildMode" "Debug"
    let targetWithEnv target env = sprintf "%s:%s" target env

    let setParams defaults =
        { defaults with
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "Platform", "Any CPU"
                    "Configuration", buildMode()
                ]
        }

Target "Clean" (fun _ ->
  let clean config = {(setParams config) with Targets = ["Clean"]}
  build clean mainSln
)

Target "Build" (fun _ ->
  let rebuild config = {(setParams config) with Targets = ["Build"]}
  build rebuild mainSln
)

open FileUtils
// open ProcessHelper
open System.Diagnostics

Target "Demo" (fun _ ->
    let curDir = pwd()

    let procCfg i (info:ProcessStartInfo) = 
      info.CreateNoWindow <- false
      info.UseShellExecute <- true
      info.FileName <- "cmd"
      info.WorkingDirectory <- curDir @@ (sprintf "Service%d/bin/debug" i)
      info.Arguments <- sprintf "/C service%d.exe" i

    // run both services
    [1..2] |> List.map procCfg |> List.iter StartProcess

    printfn "Remember to close each service before building again!"
)

"Build"
  ==> "Demo"

RunTargetOrDefault "Build"
