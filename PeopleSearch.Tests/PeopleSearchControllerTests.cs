using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearch.Controllers;
using PeopleSearch.Repositories;
using PeopleSearch.Models;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PeopleSearch.Tests
{
    [TestClass]
    public class PeopleSearchControllerTests
    {
        [TestMethod]
        public void Test_SearchPersons()
        {
            // Arrange
            var repo = Substitute.For<IDataRepository<Person>>();
            var controller = new PeopleController(null, repo);

            var peopleList = new List<Person>
            {
                new Person{Id=1, FullName="Person1", Address="Somewhere", Age=100, Interests = "None", PicturePath="1.jpg"},
                new Person{Id=2, FullName="Person2", Address="Somewhere", Age=20, Interests = "Stuff", PicturePath="2.jpg"}
            };

            var people = new TestAsyncEnumerable<Person>(peopleList);

            repo.GetAll().Returns(people.AsQueryable());

            // Act
            var result = controller.SearchPersons("Person2") as Task<ActionResult<IEnumerable<Person>>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(peopleList[1], result.Result.Value.ElementAt(0));
        }
    }

    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }
    }
}
