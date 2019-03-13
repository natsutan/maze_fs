// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。
open System 
open maze_lib
open maze_lib.Conv

let rnd = Random()

let btree(grid:maze_lib.Grid) = 
    let (row, col) = grid.shape()
    for i=0 to row - 1 do 
        for j=0 to col - 1 do
            let cell = grid.grid.[i,j]
            match (i, j) with
            | (r, c) when r = (row - 1) && c = (col - 1) -> ()
            | (r, _) when r = (row - 1) -> cell.bilink(cell.east)
            | (_, c) when c = (col - 1) -> cell.bilink(cell.north)
            | (r, c) -> let toss = rnd.Next(2)
                        match toss with
                        | 1 -> cell.bilink(cell.north)
                        | _ -> cell.bilink(cell.east)
    grid

[<EntryPoint>]
let main argv =
    let g = Grid(4, 4)
    g.init()

    btree g |> to_png_def 
 
    printfn "btree %O" g



    0 // 整数の終了コードを返します
