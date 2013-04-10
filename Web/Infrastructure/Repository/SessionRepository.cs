using System.Dynamic;
using System.Web;
using Web.Models;

// NOTE: source code from http://www.codeproject.com/Articles/191422/Accessing-ASP-NET-Session-Data-Using-Dynamics
namespace Web.Infrastructure.Repository
{
    /// <summary>
    /// Singleton class to wrap session. Session stores user's name and passwrod from database
    /// </summary>
    public sealed class SessionRepository : DynamicObject
    {
        private SessionRepository() { }

        private static readonly object syncObject = new object();

        /// <summary>
        /// The repository
        /// </summary>
        private static readonly SessionRepository SessionBag;

        /// <summary>
        /// Initializes the <see cref="SessionRepository"/> class.
        /// </summary>
        static SessionRepository()
        {
            SessionBag = new SessionRepository();
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        private HttpSessionStateBase Session
        {
            get { return new HttpSessionStateWrapper(HttpContext.Current.Session); }
        }


        /// <summary>
        /// Gets the current session.
        /// </summary>
        /// <value>
        /// The current session.
        /// </value>
        public static dynamic CurrentSession
        {
            get { return SessionBag; }
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            lock (syncObject)
            {
                result = Session[binder.Name];
            }

            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            lock (syncObject)
            {
                Session[binder.Name] = value;
            }
            return true;
        }

        /// <summary>
        /// Gets the user credentials.
        /// </summary>
        /// <returns></returns>
        public static LoginModel GetUserCredentials()
        {
            LoginModel user;
            lock (syncObject)
            {
                user = CurrentSession.UserCredentials as LoginModel;
            }
            return user;
        }


        /// <summary>
        /// Sets the user credentials.
        /// </summary>
        /// <param name="user">The user.</param>
        public static void SetUserCredentials(LoginModel user)
        {
            lock (syncObject)
            {
                CurrentSession.UserCredentials = user;
            }

        }

        /// <summary>
        /// Deletes the information about logined user from Session.
        /// </summary>
        public static void ClearUser()
        {
            lock (syncObject)
            {
                CurrentSession.UserCredentials = null;
            }
        }
    }
}