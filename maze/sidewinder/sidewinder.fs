open System 
open maze_lib
open maze_lib.Conv

// F# の詳細については、http://fsharp.org を参照してください
// 詳細については、'F# チュートリアル' プロジェクトを参照してください。


let rnd = Random()

let sidewinder(grid:maze_lib.Grid) = 
    let (row, col) = grid.shape()
    let mutable run : Cell List = []
    for i=0 to row - 1 do 
        for j=0 to col - 1 do
            let cell = grid.grid.[i,j]
            run <- cell :: run

            let at_eastern_bound:bool = cell.east = None
            let at_northern_bound:bool = cell.north = None
            let toss = rnd.Next(2)
            
            let should_close_out:bool = at_eastern_bound || ((not at_northern_bound) && (toss = 0 ))
            
            if should_close_out then
                let len = List.length run
                let selected = run.[rnd.Next(len)]
                selected.bilink(selected.north)
                run <- []                
             else
                 cell.bilink(cell.east)
            
            
    grid

[<EntryPoint>]
let main argv = 

    let g = Grid(15, 15)
    g.init()

    sidewinder g |> to_png_def
    printfn "sidewinder %O" g
    0 // 整数の終了コードを返します
