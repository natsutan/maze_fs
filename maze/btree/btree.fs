﻿// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。
open maze_lib

[<EntryPoint>]
let main argv =
    let a = maze_lib.Cell(0, 1)
        
    
    printfn "btree %A" a.east
    0 // 整数の終了コードを返します
