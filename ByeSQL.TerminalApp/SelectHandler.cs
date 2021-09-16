using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;

namespace ByeSQL.TerminalApp.Handlers
{
    class SelectHandler : IContextHandler
    {
        private Stack<Stack<string>> scopes = new Stack<Stack<string>>();
        private Stack<string> curScope;

        private List<string> skips = new List<string>{ "SELECT", "FROM", "<EOF>", ";" };

        public void Scope()
        {
            this.curScope = new Stack<string>();
        }

        public void DeScope()
        {
            this.scopes.Push(this.curScope);
            this.curScope = null;
        }

        public void Handle(IParseTree obj)
        {
            this.reScopeOnSkip(obj);
            this.curScope.Push(obj.GetText());
        }

        private void reScopeOnSkip(IParseTree obj)
        {
            if (this.skips.Contains(obj.GetText()))
            {
                this.DeScope();
                this.Scope();
            }
        }

        public void Debug()
        {
            while (this.scopes.Count > 0)
            {
                this.curScope = this.scopes.Pop();

                while (this.curScope.Count > 0)
                {
                    Console.WriteLine(this.curScope.Pop().ToString());
                }
            }
        }
    }
}
