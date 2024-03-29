﻿namespace maze_lib
open System
open System.Drawing
//open Array2D

type Cell(row:int, col:int) =
    member val row:int = row with get
    member val col:int = col with get
    member val path:Cell List = [] with get, set
    member val north: Cell option = None with get,set
    member val south: Cell option = None with get,set
    member val east: Cell option = None with get,set
    member val west: Cell option = None with get,set

    member x.link(c:Cell option) = match c with
                                   | None -> ()
                                   | Some(cc) -> x.path <- cc :: x.path
                                   

    member x.bilink(c:Cell option) = match c with
                                     | None -> ()
                                     | Some(cc) -> x.link(c)
                                                   cc.link(Some(x))
    member x.path_find(c:Cell, l:Cell List) : bool = match l with
                                                     | [] -> false
                                                     | x::xs -> if x.row = c.row && x.col = c.col then true
                                                                else x.path_find(c, xs) 


    member x.is_linked(c:Cell option) = match c with 
                                        | None -> false
                                        | Some(cc) -> x.path_find(cc, x.path)

    member x.ref_to_str_n() = match x.north with 
                                | None -> ""
                                | Some(c) -> sprintf "N:(%d, %d)" c.row c.col

    member x.ref_to_str_s() = match x.south with 
                                | None -> ""
                                | Some(c) -> sprintf "S:(%d, %d)" c.row c.col

    member x.ref_to_str_e() = match x.east with 
                                | None -> ""
                                | Some(c) -> sprintf "E:(%d, %d)" c.row c.col

    member x.ref_to_str_w() = match x.west with 
                                | None -> ""
                                | Some(c) -> sprintf "W:(%d, %d)" c.row c.col

    member x.ref_to_str(c:Cell) = sprintf "(%d %d) " c.row c.col

    member x.path_to_str() = List.map x.ref_to_str x.path
                             |> String.concat " "


    override  x.ToString() = sprintf "Cell(%d, %d) Link " x.row x.col + x.path_to_str() 
                             
    
type Grid(row:int, col:int) =
    let row:int = row
    let col:int = col
    member x.shape() = (row, col)
    member val grid = Array2D.init row col (fun i j -> Cell(i, j))

    member x.init() = for i=0 to row - 1 do 
                        for j=0 to col - 1 do
                            let c = x.grid.[i,j]
                            if i < (row - 1) then c.north <- Some(x.grid.[i+1,j])
                            if i > 0 then c.south <- Some(x.grid.[i-1,j])
                            if j < (col - 1) then c.east <- Some(x.grid.[i,j+1])
                            if j > 0 then c.west <- Some(x.grid.[i,j-1])

                            
    override  x.ToString() = let mutable output = "\n+"
                             for i=0 to col - 1 do
                                output <- output + "---+"
                             output <- output + "\n"

                             for i in row - 1 .. -1 .. 0 do
                                let mutable top = "|"
                                let mutable bottom = "+"
                                for j=0 to col - 1 do
                                    let cell = x.grid.[i,j]
                                    let body = "   "
                                    let corner = "+"
                                    let east_bound = if cell.is_linked(cell.east) then " " else "|"                                    
                                    let south_bound = if cell.is_linked(cell.south) then "   " else "---"
                                    top <- top + body + east_bound
                                    bottom <- bottom + south_bound + corner

                                output <- output + top + "\n"
                                output <- output + bottom + "\n"

                             output
    module Conv = 
        let to_png(g:Grid, filename:string, cellsize:int) =
            let row, col = g.shape()
            let width = col * cellsize
            let height = row * cellsize
            let pen = Pens.Black
            let bmp = new Bitmap(width+10, height+10)
            let gra = Graphics.FromImage(bmp)
            
            for i=0 to row - 1 do 
                for j=0 to col - 1 do
                    let c = g.grid.[i,j]
                    
                    let x1 = c.col * cellsize
                    let y1 = height - c.row * cellsize
                    let x2 = (c.col + 1) * cellsize
                    let y2 = height - (c.row + 1) * cellsize                   
                    
                    match c.north with
                    | None -> gra.DrawLine(pen, x1, y2, x2, y2)
                    | Some(cc) -> ()
                    match c.west with
                    | None -> gra.DrawLine(pen, x1, y2, x1, y1)
                    | Some(cc) -> ()
                                      
                    if not (c.is_linked c.east) then
                        gra.DrawLine(pen, x2, y2, x2, y1)
                    
                    if not (c.is_linked c.south) then
                        gra.DrawLine(pen, x1, y1, x2, y1)
                  
            
            bmp.Save(filename)
            bmp.Dispose()
            ()
                             
            
        
        let to_png_def g = to_png(g, "maze.png", 50)
       

//let bmp = new Bitmap(@"filename")

                            