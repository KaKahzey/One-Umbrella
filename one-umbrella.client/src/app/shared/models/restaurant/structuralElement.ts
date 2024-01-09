export interface StructuralElment {
    id : number,
    rowIndex : number,
    columnIndex : number,
    type : number
    /* 
     * 1 pour mur
     * 2 pour fenêtre
     * 3 pour escalier
     * 4 pour porte
     * 5 pour porte des toilettes
     * 6 pour porte des cuisines
     * 7 pour bar
     * 8 pour tv
    */
}