# ByeSQL

This was an attempt at making a version of MySQL capable of running fully in-memory and take slow disks out of the equation for optimal perfomance to see if it could be used to speed up a massive suite of end-to-end integration tests.

Eventually turned out to be easier just backing the MySQL by tmpfs, which was made easy with Docker (also included in this repo), so abandoned for now.
Running on tmpfs did show significant improvement, but not enough.

Sorry if some of the code becomes a bit janky in some parts, I don't know .Net very well, knew nothing about ANTLR or MySQL grammars, and was under time pressure on this.

That being said, you have to appreciate [what I did to the ANTLR interface](ByeSQL.TerminalApp/TestVisitor.cs) :p.

Basically it's just recursion combined with some meta-programming, but I call this application of it "interface folding".