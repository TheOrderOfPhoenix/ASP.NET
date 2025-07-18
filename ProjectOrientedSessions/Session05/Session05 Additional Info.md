
## What is CORS? (Chat GPT)
### CORS (Cross-Origin Resource Sharing)

**What it is:** CORS is a browser security feature that blocks requests to a different domain unless explicitly allowed by the server.
### 🔐 What is CORS **really** for?

CORS is **not** about protecting the backend server.  
It’s about **protecting users** from **malicious websites** using their browser as a weapon.

---

### 🧠 Imagine this attack:

You’re logged into your **bank** in one browser tab (`bank.com`).

Now, you visit a shady website in another tab (`evil.com`). That site has JavaScript that tries to send this:

```js
fetch('https://bank.com/api/transfer?amount=5000&to=hacker', {
  credentials: 'include'  // it sends your bank cookies!
});
```

➡️ If the browser allowed this freely, the request would go through **using your login session**, and you’d lose money.

---

### 💥 Enter CORS

So the browser says:

> “Hold on. This JavaScript is from `evil.com`, and it’s trying to talk to `bank.com`. I won’t let that happen **unless `bank.com` says it’s okay**.”

That’s why the **backend server** must respond with something like:

```http
Access-Control-Allow-Origin: https://mytrusteddomain.com
```

Only then will the browser say, “Okay, go ahead.”

---

### So the purpose of CORS is:

✅ To **restrict browsers** from sending or accepting responses from **cross-origin** sources  
❌ Not to protect the backend  
❌ Not to restrict Postman, curl, servers, or mobile apps

---

### 🔄 In Dev Work (like your React + API case):

- You’re running React at `http://localhost:5173`
- You’re running ASP.NET Core API at `https://localhost:7001`
- The browser sees this as two **different origins** → blocks the request unless CORS is enabled on the API.
    
---

### 🧪 Why Postman works:
- Postman isn’t a browser    
- Postman doesn’t care about same-origin policy
- Postman just sends requests like your backend would    

---

### ✅ Conclusion:

- **CORS is a browser feature to protect users**
- **It restricts frontend JavaScript from calling other domains unless explicitly allowed**
- **You must configure your server to say “Yes, I allow your frontend to talk to me”**


## Feature-based Folder Structure
> 
## Explanation of the `agent.ts`
>
## React.FC

### what is `React.FC`?

- `React.FC` (or `React.FunctionComponent`) is a **TypeScript type** that you can use to type your functional React components.
    
- It **tells TypeScript** that:
    - This component is a function
    - It **receives props** (in your case, `Props`)
    - It **returns JSX** (it returns something React can render)

---

### Why use it?

Here’s what you get when you use `React.FC`:
1. ✅ **Prop typing** — You get auto-complete and error checking for props.
2. ✅ **Children** are automatically included. (More on this below.)
3. ✅ **Cleaner code** because TypeScript understands the shape of the component.

---

### Without `React.FC`

You could just write:

```tsx
const TransportationCard = ({ transportation }: Props) => { ... }
```
You lose some "extra typing safety" like automatic `children` typing.

---

### Small Detail: `children`

When you use `React.FC`, **TypeScript automatically** allows your component to accept `children` too — even if you didn’t define it in your `Props`.

For example:

```tsx
<TransportationCard transportation={t}>
  <p>Hello</p>   // This would be valid automatically
</TransportationCard>
```

Because `children` is **always** part of a `React.FC`.

👉 If you **don't** use `React.FC`, and you want to accept `children`, you have to **manually** add it to your props.

---

### Some developers today...
**Some people** (even in big companies) prefer **NOT** to use `React.FC` anymore because:
- It **forces children** even when you don’t want children.
- It's **a little bit redundant** — you can already just type props without it.

> So in modern codebases, **both styles are OK** — it’s just a preference.

---

### Quick Summary:

| Using `React.FC`                             | Not using `React.FC`                            |
| :------------------------------------------- | :---------------------------------------------- |
| Good for simple, typed functional components | Good if you want full manual control over props |
| Auto-includes `children` prop                | You must manually add `children` if needed      |
| Easy and quick                               | More customizable                               |

---

## `CityDropdown` Component:

### 1. **State Variables**

```tsx
const [cities, setCities] = useState<City[]>([]);
const [selectedCity, setSelectedCity] = useState<number | undefined>();
```

- `cities`: holds the list of cities retrieved from the backend (starts empty `[]`).
- `selectedCity`: holds the currently selected city’s ID (`number`) or `undefined` if nothing is selected yet.
    

---

### 2. **Fetching Cities on Mount**

```tsx
useEffect(() => {
  agent.Cities.list()
    .then(setCities)
    .catch((err) => console.error("error loading cities", err));
}, []);
```

- When the component **mounts** (`[]` dependency array = run once), it calls `agent.Cities.list()`.    
- `agent.Cities.list()` presumably returns a promise that resolves to an array of `City` objects.
- On success → `setCities` updates the `cities` state.
- On failure → logs an error to the console.

---

### 3. **Rendering the Dropdown**

```tsx
<select
  value={selectedCity}
  onChange={(e) => setSelectedCity(Number(e.target.value))}
>
  <option value="">Select a City</option>
  {cities.map((city) => (
    <option key={city.id} value={city.id}>
      {city.title}
    </option>
  ))}
</select>
```

- Renders a `<select>` (dropdown).
- Its value is bound to `selectedCity`.
- When the user changes the selection (`onChange`), it updates `selectedCity` by converting the selected `value` from a string to a number (`Number(e.target.value)`).
- The dropdown always starts with a placeholder option: **"Select a City"**.
- It dynamically creates an `<option>` for each city in the `cities` array:
    - `key` and `value` are the city's `id`.
    - Displayed text is the city's `title`.        

---

### What is `useEffect`?

🔹 **`useEffect` is a React Hook**.  
It **tells React to run some code after the component renders**.


In code:

```tsx
useEffect(() => {
  agent.Cities.list()
    .then(setCities)
    .catch((err) => console.error("error loading cities", err));
}, []);
```

- This function (`() => { ... }`) **runs right after** the component is first shown (because of `[]` — the empty array).
- Inside it, you are **calling your API** (`agent.Cities.list()`) to get the cities.
- When the server **responds with a list of cities**, you **save** them into your component's memory (state) by calling `setCities(data)`.

---

### What is `useState`?

🔹 **`useState` is another React Hook**.  
It **creates a piece of memory** for your component.

In code:

```tsx
const [cities, setCities] = useState<City[]>([]);
const [selectedCity, setSelectedCity] = useState<number | undefined>();
```

Here’s what is happening:
- `cities` is a variable that starts as an **empty array** (`[]`).
- `setCities` is a **function** you use to **change** the value of `cities`.

Same with `selectedCity`:
- `selectedCity` starts as `undefined` (nothing selected yet).
- `setSelectedCity` lets you **update** which city is selected.
    

---

## `TransportationCard` Component

### 1. **The Component Function**

```tsx
const TransportationCard: React.FC<Props> = ({ transportation }) => {
```

- This is a **React Functional Component**.
- It takes `transportation` from props (destructured directly).    
- It **returns JSX** that shows the transportation info in a styled card.

---
### 2. **Inside the JSX**

#### 2.1 Left Side — Price and Button

```tsx
<div className="flex flex-col items-center">
  <div className="text-blue-600 font-bold text-lg">
    {transportation.price} Toman
  </div>
  <button className="bg-blue-500 text-white px-4 py-2 rounded-md mt-2 hover:bg-blue-600">
    Select Ticket
  </button>
</div>
```

- Shows the **price** (`price` field) styled with blue, bold text. 
- Has a **Select Ticket** button — blue, rounded, changes shade on hover.

---

#### 2.2 Middle — Trip Info

```tsx
<div className="flex-1 mx-4 text-center">
  <div className="font-semibold text-gray-700">
    {transportation.companyTitle}
  </div>
  <div className="flex items-center justify-center mt-2">
    <div className="mx-2">{transportation.fromCityTitle}</div>
    <span className="text-gray-400">→</span>
    <div className="mx-2">{transportation.toCityTitle}</div>
  </div>
  <div className="text-sm text-gray-500 mt-1">
    {new Date(transportation.startDateTime).toLocaleDateString("en", {
      hour: "2-digit",
      minute: "2-digit",
    })}
  </div>
</div>
```

- Shows the **company name**.    
- Shows **From → To** cities in a neat way with an arrow (`→`) between them.
- Shows the **departure time**:
    - `startDateTime` is parsed using `new Date(...)`.
    - `toLocaleDateString("en", { hour: "2-digit", minute: "2-digit" })` formats it to show just **hours and minutes**.

---
#### 2.3 Right Side — Company Logo

```tsx
<div className="w-12 h-12">
  <img
    src="/images/company-placeholder.png"
    alt="company"
    className="w-full h-full object-contain"
  />
</div>
```

- Displays a small company **logo image** (placeholder image for now).
- `object-contain` keeps the image inside the box without stretching.

## `TrasnportationSearchForm` Component

### 🧠 Main Concepts Used:

| Feature                   | Purpose                                                                      |
| :------------------------ | :--------------------------------------------------------------------------- |
| `useState`                | To **store and manage** the form inputs, cities, results, and loading status |
| `useEffect`               | To **load the list of cities once** when the component appears               |
| **Typing with models**    | `City`, `TransportationSearchRequest`, `TransportationSearchResult`          |
| **Event handling**        | To update form values and trigger the search                                 |
| **Conditional rendering** | Show "Loading", "No results", or "Results" dynamically                       |
|                           |                                                                              |

---

### 📦 Let's break down the code:

#### 1. ✍️ States

```tsx
const [searchResults, setSearchResults] = useState<TransportationSearchResult[]>([]);
const [loading, setLoading] = useState(false);
const [cities, setCities] = useState<City[]>([]);
const [form, setForm] = useState<TransportationSearchRequest>({
  fromCityId: undefined,
  toCityId: undefined,
  startDate: null,
  endDate: null,
  vehicleTypeId: undefined,
});
```

You define **4 states**:

- `searchResults`: List of found transportations
- `loading`: True/false if waiting for API
- `cities`: List of available cities
- `form`: The form input values the user is selecting

All typed properly ✅

---

#### 2. 📡 Fetch cities automatically (useEffect)

```tsx
useEffect(() => {
  agent.Cities.list().then(setCities);
}, []);
```

**Meaning**:
- When the page **first loads**, it **calls the API** to get cities.
- `agent.Cities.list()` calls your backend, and when it gets the cities, it puts them into `cities` state.    
- The empty `[]` **dependency array** means this happens **only once**, not every time anything changes.

---

#### 3. ✏️ Handle input changes

```tsx
const handleChange = (e: React.ChangeEvent<HTMLSelectElement | HTMLInputElement>) => { ... }
```

Whenever a user types/selects:
- You detect **which field** (`name`) and **what value** (`value`) they changed
- Update the `form` state accordingly:    
    - `fromCityId` and `toCityId` are converted to numbers (`parseInt`)        
    - `startDate` and `endDate` allow null
    - Other fields (if any) are copied directly

---

#### 4. 🔍 Handle Search button

```tsx
const handleSearch = () => { ... }
```

When the user clicks **Search**:
- Set `loading` to `true`    
- Call the backend API `agent.TransportationSearch.search(form)`
- When the result comes back:
    - Save it into `searchResults`
- If error: log it
- Finally, set `loading` to `false` again

---

#### 5. 🖥️ Return (render) JSX
You build a UI:

- **Vehicle Types** (Bus, Train, Airplane) selectable with a click → sets `vehicleTypeId`
- **From City** and **To City** dropdowns
- **Start and End Date** inputs
- **Search Button** to trigger the search
- **Result area** that shows:
    - If loading: "Loading..."
    - If no results: "No results found"        
    - If results: List of `TransportationCard` components for each found item.
        

---






