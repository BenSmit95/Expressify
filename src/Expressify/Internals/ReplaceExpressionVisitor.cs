using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Expressify.Internals;

public class ReplaceExpressionVisitor(Expression search, Expression replace) : ExpressionVisitor
{
    [return: NotNullIfNotNull("node")]
    public override Expression? Visit(Expression? node)
    {
        return node == search ? replace : base.Visit(node);
    }
}