mergeInto(LibraryManager.library, {
  SendUnityMessage: function (str) {
    if (window.sendUnityMessage) {
      window.sendUnityMessage(Pointer_stringify(str));
    }
  },
});
