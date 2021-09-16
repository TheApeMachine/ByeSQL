using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;

namespace ByeSQL.TerminalApp.Handlers
{
    class QueryHandler : BaseHandler, IContextHandler
    {
        private ScopeHandler scopeHandler = new ScopeHandler();
        public QueryHandler()
        {
            this.ctxMap.Add("CREATE", Ctx.CREATE);
            this.ctxMap.Add("INSERT", Ctx.INSERT);
            this.ctxMap.Add("SELECT", Ctx.SELECT);
            this.ctxMap.Add("JOIN", Ctx.JOIN);
        }

        private void Contextualize() 
        {
            // First rescope.
            this.Scope();

            if (this.curContext.Count > 0)
            {
                this.contexts.Push(this.curContext);
                this.contextCache.Push(this.curCtx);
            }

            this.curContext = new Stack<Stack<Stack<string>>>();
        }

        private void Scope()
        {
            // We create a new current scope stack in which we gather the input
            // from the tree node,
            if (this.curScope.Count > 0)
            {
                this.scopes.Push(this.curScope);
                this.curContext.Push(this.scopes);
            }

            this.curScope = new Stack<string>();
        }

        public void Handle(IParseTree obj)
        {
            string objTxt = obj.GetText();

            if (this.contexts == null) this.Contextualize();
            else if (this.terminators.Contains(objTxt)) return;
            else if (this.ctxSwitchers.Contains(objTxt)) this.setContext(obj);
            else if (this.keywords.Contains(objTxt)) this.setScope(obj);
            else if (this.delimiters.Contains(objTxt)) return;
            else this.curScope.Push(objTxt);
        }

        private void setContext(IParseTree obj)
        {
            string objTxt = obj.GetText();
            Ctx objCtx = this.ctxMap[objTxt];

            if (objCtx != this.curCtx && objCtx != Ctx.UNKNOWN)
            {
                this.curCtx = objCtx;
                this.Contextualize();
            }
        }

        private void setScope(IParseTree obj)
        {
            this.Scope();
        }

        public void Execute(bool debug)
        {
            this.Contextualize();

            while(this.contexts.Count > 0)
            {
                this.curContext = this.contexts.Pop();

                if (debug) Console.WriteLine(this.curCtx);

                this.scopes = this.curContext.Pop();
                this.curCtx = this.contextCache.Pop();

                while (this.scopes.Count > 0)
                {
                    if (debug) Console.WriteLine("\nSCOPE:");
                    this.curScope = this.scopes.Pop();
                    this.scopeHandler.Execute(this.curCtx, debug, this.curScope);
                }
            }
        }
    }
}
