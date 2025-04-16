using System.Linq.Expressions;

namespace Expressify.UnitTests;

public class ExpressionReplace
{
    [Fact]
    public void GivenAnyArgumentIsNull_ThrowsException()
    {
        var expr1 = Expression.Constant(2);
        var expr2 = Expression.Constant(3);
        
        Assert.Throws<ArgumentNullException>(() => expr1.Replace(expr2, null!));
        Assert.Throws<ArgumentNullException>(() => expr1.Replace(null!, expr2));
        Assert.Throws<ArgumentNullException>(() => ((Expression)null!)!.Replace(expr1, expr2));
    }

    [Fact]
    public void GivenSearchEqualsNode_ThrowsException()
    {
        var expr1 = Expression.Constant(2);
        var expr2 = Expression.Constant(3);
        Assert.Throws<ArgumentException>(() => expr1.Replace(expr1, expr2));
    }

    [Fact]
    public void GivenSearchIsFound_ReplacesAllInstancesOfSearchExpression()
    {
        var searchExpression = Expression.Constant(2);
        var replaceExpression = Expression.Constant(3);

        var node = Expression.Lambda<Func<int>>(Expression.Add(searchExpression, searchExpression));

        var replacedExpression = node.Replace(searchExpression, replaceExpression);

        var result = replacedExpression.Compile()();
        
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void GivenSearchIsFound_ReturnsNewExpression()
    {
        var searchExpression = Expression.Constant(2);
        var replaceExpression = Expression.Constant(3);

        var node = Expression.Lambda<Func<int>>(Expression.Add(searchExpression, searchExpression));

        var replacedExpression = node.Replace(searchExpression, replaceExpression);
        
        Assert.NotEqual(replacedExpression, node);
    }

    [Fact]
    public void GivenSearchIsNotFound_ReturnsUnchangedNode()
    {
        var node = Expression.Lambda<Func<int>>(Expression.Add(Expression.Constant(5), Expression.Constant(5)));

        var replacedExpression = node.Replace(Expression.Constant(4), Expression.Constant(4));
        
        var result = replacedExpression.Compile()();
        
        Assert.Equal(replacedExpression, node);
        Assert.Equal(10, result);
    }
}