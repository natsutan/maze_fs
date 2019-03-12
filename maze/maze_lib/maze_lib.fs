namespace maze_lib
//open Array2D

type Cell(row:int, col:int) =
    member val row:int = row with get
    member val col:int = col with get
    member val link:Cell List = []
    member val north: Cell option = None with get,set
    member val south: Cell option = None with get,set
    member val east: Cell option = None with get,set
    member val west: Cell option = None with get,set
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



    override  x.ToString() = sprintf "Cell(%d, %d) " x.row x.col 
                             + x.ref_to_str_n()
                             + x.ref_to_str_s()
                             + x.ref_to_str_e()
                             + x.ref_to_str_w()
                             
    
type Grid(row:int, col:int) =
    let row:int = row
    let col:int = col
    member x.shape() = (row, col)
    member val grid = Array2D.init row col (fun i j -> Cell(i, j))

    member x.init() = for i=0 to row - 1 do 
                        for j=0 to col - 1 do
                            printfn "i,j = %d,%d row = %d col = %d" i j row col
                            let c = x.grid.[i,j]
                            if i < (row - 1) then c.north <- Some(x.grid.[i+1,j])
                            if i > 0 then c.south <- Some(x.grid.[i-1,j])
                          
                            if j < (col - 1) then c.east <- Some(x.grid.[i,j+1])
                            if j > 0 then c.west <- Some(x.grid.[i,j-1])

                            
