using Antlr4.Runtime.Tree;

namespace ByeSQL.TerminalApp.Handlers
{
    public interface IContextHandler
    {
        void Scope();
        void DeScope();
        void Handle(IParseTree obj);
        void Debug();
    }
}
