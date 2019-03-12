namespace maze_lib
//open Array2D

type Cell(row:int, col:int) =
    let row:int = row
    let col:int = col
    member val link:Cell List = []
    member val north: Cell option = None with get,set
    member val south: Cell option = None with get,set
    member val east: Cell option = None with get,set
    member val west: Cell option = None with get,set
    
    
type Grid(row:int, col:int) =
    let row:int = row
    let col:int = col
    member val grid = Array2D.init row col (fun i j -> Cell(i, j))

    