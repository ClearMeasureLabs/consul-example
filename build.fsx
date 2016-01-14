#r "packages/FAKE/tools/fakelib.dll"

open Fake

[<AutoOpen>]
module Config =
    let prjName = "ConsulServices"
    let mainSln = prjName + ".sln"
    let mainPrj = prjName

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

RunTargetOrDefault "Build"
