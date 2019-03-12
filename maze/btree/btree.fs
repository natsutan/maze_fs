// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。
open maze_lib

[<EntryPoint>]
let main argv =
    let g = maze_lib.Grid(2, 2)
    g.init()

    printfn "btree %O" g.grid.[0, 1]
    
    printfn "btree %O" g.grid.[1, 1]


    0 // 整数の終了コードを返します
