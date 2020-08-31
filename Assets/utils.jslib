mergeInto(LibraryManager.library, {
  OnMessage: function (str) {
    if (window.sendUnityMessage) {
      window.sendUnityMessage(Pointer_stringify(str));
    }
  },
});
