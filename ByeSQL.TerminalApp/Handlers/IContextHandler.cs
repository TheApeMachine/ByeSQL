using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace ByeSQL.TerminalApp.Handlers
{
    public interface IContextHandler
    {
        void Handle(IParseTree obj);
        void Execute(bool debug);
    }

    public class BaseHandler
    {
        internal enum Ctx
        { UNKNOWN, CREATE, INSERT, SELECT, JOIN }

        internal Dictionary<string, Ctx> ctxMap = new Dictionary<string, Ctx>();

        internal Stack<Ctx> contextCache = new Stack<Ctx>();

        internal Stack<Stack<Stack<Stack<string>>>> contexts = new Stack<Stack<Stack<Stack<string>>>>();
        internal Stack<Stack<string>> scopes = new Stack<Stack<string>>();

        internal Stack<Stack<Stack<string>>> curContext = new Stack<Stack<Stack<string>>>();
        internal Ctx curCtx;
        internal Stack<string> curScope = new Stack<string>();

        internal List<string> types = new List<string> { "TIMESTAMP", "VARCHAR", "NULL", "KEY", "INT" };
        internal List<string> keywords = new List<string> { "FROM", "WHERE" };
        internal List<string> variables = new List<string> { "CURRENT_TIMESTAMP" };
        internal List<string> modifiers = new List<string> { "DEFAULT" };
        internal List<string> delimiters = new List<string> { "(", ")", "," };
        internal List<string> terminators = new List<string> { ";", "<EOF>" };
        internal List<string> ctxSwitchers = new List<string> { "CREATE", "INSERT", "SELECT", "JOIN" };

        public BaseHandler()
        {
            this.ctxMap.Add("UNKNOWN", Ctx.UNKNOWN);
        }
    }
}
