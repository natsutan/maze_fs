open System 
open maze_lib
open maze_lib.Conv

// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。

[<EntryPoint>]
let main argv = 

    let g = Grid(4, 4)
    g.init()

    0 // 整数の終了コードを返します
