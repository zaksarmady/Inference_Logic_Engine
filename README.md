## Inference_Logic_Engine
Inference Engine for propositional logic in software based on the Truth Table (TT) checking, and Backward Chaining (BC) and Forward Chaining (FC) algorithms.
 	
## Features
Read and parse a Horn-form KB within a .txt file. Inference engine has Truth table, Forward chaining and Backwards Chaining implemented. Truth table implemented in a general knowledge-based manner. Some basic help is also implemented in the command interface. 

## Bugs	
Bugs: TT can take a long time to implement large test cases with a large number of clauses. It may still work for these large cases, however processing time is much too long for it to work efficiently. Additionally, the command line interface requires the user to press the enter key twice, instead of once. This is due to a bug in the interface method. 

## Missing Features
Missing: Knowledge base Truth table does not support disjunction ||,  parenthesis (), not ~, Biconditional <=>. The only symbols supported are "&" (and) and "=>" (implies). 
	
The program is run through a command line interface. Below is an example of how to run it:
<filename> <Logic Method> <test file>

For example: iEngine.exe TT test test4.txt

The .exe file needed to execute the program is located in \...Inference_Engine\bin\Debug\netcoreapp3.1
	
If a user wants to edit or add more test files, they can be found in ...\Inference_Engine\tests