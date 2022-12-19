export enum TableContent {
  pdbID = "{\"attrID\":\"pdbID\",\"attrName\":\"PDB ID\",\"attrType\":\"meta\",\"isOperator\":false,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":5}",
  authorName = "{\"attrID\":\"authorName\",\"attrName\":\"Author name\",\"attrType\":\"meta\",\"isOperator\":true,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":5}",
  pdbDeposition = "{\"attrID\":\"pdbDeposition\",\"attrName\":\"PDB deposition\",\"attrType\":\"meta\",\"isOperator\":true,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":2}",
  keyword = "{\"attrID\":\"keyword\",\"attrName\":\"Keyword\",\"attrType\":\"meta\",\"isOperator\":false,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":1}",
  expMethod = "{\"attrID\":\"expMethod\",\"attrName\":\"Experimental method\",\"attrType\":\"meta\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"X-Ray\",\"operator\":\"\"},{\"condition\":\"NMR\",\"operator\":\"\"}],\"rowType\":\"multiSelect\",\"maxCondCount\":0}",
  molType = "{\"attrID\":\"molType\",\"attrName\":\"Molecule type\",\"attrType\":\"meta\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"DNA\",\"operator\":\"\"},{\"condition\":\"RNA\",\"operator\":\"\"},{\"condition\":\"other\",\"operator\":\"\"}],\"rowType\":\"multiSelect\",\"maxCondCount\":0}",
  sequence = "{\"attrID\":\"sequence\",\"attrName\":\"Sequence\",\"attrType\":\"struct\",\"isOperator\":true,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":1}",
  ions = "{\"attrID\":\"ions\",\"attrName\":\"Ions\",\"attrType\":\"struct\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"Na\",\"operator\":\"\"},{\"condition\":\"K\",\"operator\":\"\"},{\"condition\":\"Pt\",\"operator\":\"\"},{\"condition\":\"Tl\",\"operator\":\"\"}],\"rowType\":\"multiSelect\",\"maxCondCount\":0}",
  typeNoStrands = "{\"attrID\":\"typeNoStrands\",\"attrName\":\"Type (no. of strands)\",\"attrType\":\"struct\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"unimolecular\",\"operator\":\"\"},{\"condition\":\"bimolecular\",\"operator\":\"\"},{\"condition\":\"tetramolecular\",\"operator\":\"\"}],\"rowType\":\"multiSelect\",\"maxCondCount\":0}",
  noOfTetrads = "{\"attrID\":\"noOfTetrads\",\"attrName\":\"No. of tetrads\",\"attrType\":\"struct\",\"isOperator\":true,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":5}",
  gtractSeq = "{\"attrID\":\"gtractSeq\",\"attrName\":\"G-tract sequence\",\"attrType\":\"struct\",\"isOperator\":false,\"conditions\":[],\"rowType\":\"addable\",\"maxCondCount\":5}",
  bulges = "{\"attrID\":\"bulges\",\"attrName\":\"Bulges\",\"attrType\":\"struct\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"with bulges\",\"operator\":\"\"},{\"condition\":\"without bulges\",\"operator\":\"\"}],\"rowType\":\"radioSelect\",\"maxCondCount\":0}",
  vLoops = "{\"attrID\":\"vLoops\",\"attrName\":\"V-Loops\",\"attrType\":\"struct\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"with V-Loops\",\"operator\":\"\"},{\"condition\":\"without V-Loops\",\"operator\":\"\"}],\"rowType\":\"radioSelect\",\"maxCondCount\":0}",
  webbaDaSilva = "{\"attrID\":\"webbaDaSilva\",\"attrName\":\"Webba da Silva class\",\"attrType\":\"struct\", \"isOperator\": false, \"conditions\": [], \"rowType\": \"addable\",\"maxCondCount\":1}",
  onzClass = "{\"attrID\":\"onzClass\",\"attrName\":\"ONZ class\",\"attrType\":\"struct\",\"isOperator\":false,\"conditions\":[{\"condition\":\"any\",\"operator\":\"\"},{\"condition\":\"N-\",\"operator\":\"\"},{\"condition\":\"Z-\",\"operator\":\"\"},{\"condition\":\"O-\",\"operator\":\"\"}],\"rowType\":\"multiSelect\",\"maxCondCount\":0}"
}
