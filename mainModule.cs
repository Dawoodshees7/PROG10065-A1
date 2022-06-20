namespace Namespace {
    
    using ATMApplication = ATMApplicationModule.ATMApplication;
    //Finished
    public static class Module {
        
        public static object app = ATMApplication();
        
        static Module() {
            app.run();
        }
    }
}
