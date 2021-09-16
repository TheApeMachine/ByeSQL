using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Workbench.Parsers;
using static Workbench.Parsers.MySQLParser;

namespace ByeSQL.TerminalApp
{
    class ByeSQLTester
    {
        private HashSet<string> charsets;
        private readonly StringBuilder lastErrors;
        private SqlMode sqlMode;
        private int serverVersion;

        public ByeSQLTester()
        {
            charsets = new HashSet<string>(new string[]{
                "_utf8", "_utf8mb3", "_utf8mb4", "_ucs2",
                "_big5",   "_latin2", "_ujis", "_binary",
                "_cp1250", "_latin1"
            });
            lastErrors = new StringBuilder();
            sqlMode = SqlMode.AnsiQuotes | SqlMode.IgnoreSpace;
            serverVersion = MySqlServerVersion.MAX_SERVER_VERSION;
        }

        public void Parse(string inQuery)
        {
            IMySQLRecognizerCommon mySQLRecognizerCommon = new MySQLRecognizerCommon(serverVersion, sqlMode);
            var input = new AntlrInputStream(inQuery);
            var lexer = new MySQLLexer(input, mySQLRecognizerCommon);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MySQLParser(tokens, mySQLRecognizerCommon);
            IMySQLParserVisitor<IParseTree> visitor = new TestVisitor();

            MySQLParser.QueryContext context = parser.query();
            _ = visitor.Visit(context);
        }
    }
}
