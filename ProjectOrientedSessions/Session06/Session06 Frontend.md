
# Branching
- [ ]  Create the feature/navbar branch based on develop

# Adding a Navbar
## 🔹 What Is a Navbar?
- A **navigation bar (navbar)** is a UI element typically placed at the **top** or **side** of a web app.

- [ ] Use a `<nav>` with `flex`, `justify-between`, `items-center`.
- [ ] Add buttons or links like Home, Search, About. (the links can be empty now)

use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/shared/components)


link to project:
📂 Suggested Folder for navbar component: src/shared/components/Navbar
📂 Suggested Folder for images: public/images/

# Merge
- [ ] Create a PR and merge the current branch with develop
# Branching
- [ ]  Create the feature/routing branch based on develop

# Modifying Project From Single Component to Routed Pages


Originally, your transportation search logic and UI may have all been inside one component — which quickly becomes messy and hard to manage as your app grows.

Now we’ve **split the logic into two proper pages**:

### ✅ `SearchPage.jsx`


- Responsible only for showing the **search form**.
- Clean and minimal.
- Uses the reusable `TransportationSearchForm` component.
 - use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation/pages)    

### ✅ `SearchResultsPage.jsx`

- Responsible for **fetching and showing results**.
- Reads route parameters and query strings.
- Calls the backend using `agent`.
- Shows a loading state, handles empty results, and renders cards.
- use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation/pages)
    

This separation improves:

- Routing and navigation
- Code readability and maintainability
- Reusability of components like the search form and result cards


---




## **Create Pages into a Pages Folder**


📂 Suggested Folder for navbar component
Inside your `features/transportation` folder:
```
pages/
├── SearchPage.jsx
├── SearchResultsPage.jsx
components/
├── TransportationSearchForm.jsx
├── TransportationCard.jsx
```

### 2. **Create `SearchPage`**

Use:

```jsx
import TransportationSearchForm from "@/features/transportation/transportationSearchForm";

const SearchPage = () => {
  return (
    <div className="container mx-auto py-6">
      <TransportationSearchForm />
    </div>
  );
};

export default SearchPage;
```

Keep the form clean and layout minimal.

### 3. **Create `SearchResultsPage`**

- Use `useParams()` for URL parameters (`vehicleId`, `fromCityId`, `toCityId`)
- Use `useLocation()` and `URLSearchParams` to read query strings (`departing`, `arriving`)
- Fetch data from backend using a shared `agent`
- Display a loading state, empty message, and the result list

Code Example:
```jsx
import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import agent from "@/shared/api/agent";
import { TransportationSearchResult } from "@/shared/models/transportation/transportationSearchResult";
import TransportationCard from "@/features/transportation/transportationCard";

function useQuery() {
  return new URLSearchParams(useLocation().search);
}

const SearchResultsPage = () => {
  const { vehicleId, fromCityId, toCityId } = useParams();
  const vehicleTypeId = vehicleId ? parseInt(vehicleId, 1) : 1;
  const fromId = fromCityId ? parseInt(fromCityId, 1) : undefined;
  const toId = toCityId ? parseInt(toCityId, 1) : undefined;

  const query = useQuery();

  const departing = query.get("departing");
  const arriving = query.get("arriving");


  const [results, setResults] = useState<TransportationSearchResult[]>([]);
  const [loading, setLoading] = useState(true);

  

  useEffect(() => {

    const form = {
      vehicleTypeId,
      fromCityId: fromId,
      toCityId: toId,
      startDate: departing || null,
      endDate: arriving || null,
    };

  

    agent.TransportationSearch.search(form)
      .then(setResults)
      .catch((err) => console.error(err))
      .finally(() => setLoading(false));
  }, [vehicleId, fromCityId, toCityId, departing, arriving]);


  return (
    <div className="container mx-auto py-6">
      <h2 className="text-2xl font-bold mb-4">Search Results</h2>
      {loading ? (
        <p>Loading...</p>
      ) : results.length === 0 ? (
        <p>No results found.</p>
      ) : (
        <div className="space-y-4">
          {results.map((r) => (
            <TransportationCard key={r.id} transportation={r} />
          ))}
        </div>
      )}

    </div>
  );
};

  

export default SearchResultsPage;
```

#### 🔍 SearchResultsPage – Understanding Parameters and Arguments

This page is responsible for:

1. **Reading route parameters and query strings from the URL**
2. **Sending those values as a form to the backend**
3. **Showing the result (or loading/error message)**
    

---

##### ✅ 1. **Route Parameters**

When you define this route in `App.jsx`:

```jsx
<Route path="/:vehicleId/:fromCityId/:toCityId" element={<SearchResultsPage />} />
```

It means the URL will look like:
```
/1/21/45
```
Those values are extracted using:
```js
const { vehicleId, fromCityId, toCityId } = useParams();
```

🔹 `useParams()` comes from React Router and gives you access to the dynamic parts of the URL.

---

#####  ✅ 2. **Query String Parameters**

Suppose your full URL is:

```
/1/21/45?departing=2025-06-01&arriving=2025-06-10
```

These extra values after the `?` are **query string parameters**. They're accessed using:

```js
const query = useQuery(); // Custom helper
const departing = query.get("departing");
const arriving = query.get("arriving");
```

The helper `useQuery()` is:

```js
function useQuery() {
  return new URLSearchParams(useLocation().search);
}
```

This uses React Router's `useLocation()` to access the full URL, and then parses the query string.

---

#####  ✅ 3. **Parsing and Converting Values**

React Router gives you everything as strings. So:

```js
const vehicleTypeId = vehicleId ? parseInt(vehicleId, 10) : 1;
const fromId = fromCityId ? parseInt(fromCityId, 10) : undefined;
const toId = toCityId ? parseInt(toCityId, 10) : undefined;
```

This ensures you have **numbers**, not strings, when building your form object.

---

#####  ✅ 4. **Building the Search Form and Fetching Data**

Now all data is combined into one `form` object:

```js
const form = {
  vehicleTypeId,
  fromCityId: fromId,
  toCityId: toId,
  startDate: departing || null,
  endDate: arriving || null,
};
```

Then it sends that to the backend:

```js
agent.TransportationSearch.search(form)
  .then(setResults)
  .catch(err => console.error(err))
  .finally(() => setLoading(false));
```

---
### 4. **Modify `TransportationSearchForm`:**
use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation)

#### 1. **Removed Search Result Fetching from Inside the Component**

#### 2. **Updated `handleSearch` to Navigate with Parameters**

```tsx
const handleSearch = () => {
  if (!form.fromCityId || !form.toCityId || !form.startDate || !form.vehicleTypeId)
    return;

  const params = new URLSearchParams();

  if (form.startDate instanceof Date)
    params.append("departing", form.startDate.toISOString());
  else if (typeof form.startDate === "string")
    params.append("departing", form.startDate);

  if (form.endDate instanceof Date)
    params.append("arriving", form.endDate.toISOString());
  else if (typeof form.endDate === "string")
    params.append("arriving", form.endDate);

  navigate(
    `/${form.vehicleTypeId}/${form.fromCityId}/${form.toCityId}?${params.toString()}`
  );
};
```

**Changes made:**

- It **checks form validity** first.
    
- Then it builds a **URL using `URLSearchParams`** for `departing` and `arriving` dates.
    
- Then it calls `navigate(...)` to go to a **route like**:
    
    ```
    /1/2/3?departing=2025-06-15T00%3A00%3A00.000Z&arriving=2025-06-18T00%3A00%3A00.000Z
    ```
    

> That route (`/vehicleTypeId/fromCityId/toCityId`) will be handled by your `SearchResultPage` via `react-router`.

---

#### 3. **Used `useNavigate` from `react-router-dom`**

```tsx
import { useNavigate } from "react-router-dom";
```
    
#### 4. **Removed Result Display Section**





# Add Routing 
- [ ] Change `App.tsx` as the following:
```tsx
import Navbar from "@/shared/components/navbar";
import "./App.css";
  
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SearchPage from "@/features/transportation/pages/SearchPage";
import SearchResultsPage from "@/features/transportation/pages/SearchResultsPage";

function App() {
  return (
    <Router>
      <Navbar /> {/* Navbar will show on all pages */}
      <div className="pt-16">
        {" "}
        {/* padding top if navbar is fixed */}
        <Routes>
          <Route path="/" element={<SearchPage />} />
          <Route
            path="/:vehicleId/:fromCityId/:toCityId"
            element={<SearchResultsPage />}
          />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
```


### Explanation of `App.jsx`

#### 🔁 `Router` & `Routes`:

- Wraps the entire app in `<Router>` so that React Router can manage navigation.  
- `<Routes>` contains all the individual page routes.

---
#### 📌 Routes:

- `/`: Loads `SearchPage`. This is your home/search form.  
- `/:vehicleId/:fromCityId/:toCityId`: Loads `SearchResultsPage`. This URL carries parameters to display results based on user input.

---

#### 🎯 Navbar Placement:

- Placed **outside** `<Routes>`, so it shows on **all pages**.  
- The surrounding `<div className="pt-16">` adds space at the top so that page content isn’t hidden behind the navbar (assuming the navbar is fixed).    

---

## ✅ Checklist for Setting Up Routing

### 1. **Install React Router** (If you haven't already)

```bash
npm install react-router-dom
```
    
### 2. **Wrap Your App in Router**

```jsx
<Router>
  <Navbar />
  <Routes>
    {/* your routes here */}
  </Routes>
</Router>
```

### 3. **Define Routes**

```jsx
<Route path="/" element={<SearchPage />} />
<Route path="/:vehicleId/:fromCityId/:toCityId" element={<SearchResultsPage />} />
```

# Merge
- [ ] Create a PR and merge the current branch with develop