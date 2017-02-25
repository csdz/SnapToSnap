using System;

namespace Example
{
  internal class NotificationMessage
  {
    public string Body {
      get; set;
    }

    public string Header {
      get; set;
    }

    public override string ToString ()
    {
      return String.Format ("{0}: {1}", Header, Body);
    }
  }
}
