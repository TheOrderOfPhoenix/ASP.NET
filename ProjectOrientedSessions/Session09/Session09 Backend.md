Endpoint for increasing balance
Endpoint for reserving ticket(s) + updating transportation remaining count + adding transaction 
Endpoint for getting seats + seeing if each is reserved or not
Creating and sending a pdf downloadable ticket



### ⚠ Important Tip

If your `agent.ts` uses `useAuthStore().token` **at the top level**, remember:

- On very first load, Zustand will **rehydrate** from storage _after_ initial render, so token may be `null` until rehydration is done.
    
- Fix: either delay access until after `hasHydrated`, or refactor `agent` to inject token per request.
