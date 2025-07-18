
# 🛠️ Task Checklist

## 🚧Branching
- [ ]  Create the feature/navbar branch based on develop

## Adding a Navbar
### 🔹 What Is a Navbar?
- A **navigation bar (navbar)** is a UI element typically placed at the **top** or **side** of a web app.

- [ ] Use a `<nav>` with `flex`, `justify-between`, `items-center`.
- [ ] Add buttons or links like Home, Search, About. (the links can be empty now)

use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/shared/components)

📂 Suggested Folder for navbar component: `src/shared/components/Navbar`
📂 Suggested Folder for images: `public/images/`

## 🚧Merge
- [ ] Create a PR and merge the current branch with develop
## 🚧Branching
- [ ]  Create the feature/routing branch based on develop

## Converting Search Functionality From Single Component to Routed Pages

Originally, transportation search logic and UI may have all been inside one component — which quickly becomes messy and hard to manage as your app grows.

Now we’ve **split the logic into two proper pages**:

### ✅ `SearchPage.jsx`
- Responsible only for showing the **search form**.
- Clean and minimal.
- Uses the reusable `TransportationSearchForm` component.
- Use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation/pages)    

### ✅ `SearchResultsPage.jsx`
- Responsible for **fetching and showing results**.
- Reads route parameters and query strings.
- Calls the backend using `agent`.
- Shows a loading state, handles empty results, and renders cards.
- Use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation/pages)


This separation improves:
- Routing and navigation
- Code readability and maintainability
- Reusability of components like the search form and result cards
---

### **Create Pages into a Pages Folder**

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

- [ ] Create `SearchPage`
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
> Keep the form clean and layout minimal.

- [ ] Create `SearchResultsPage`
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

#### 🔍 `SearchResultsPage` – Understanding Parameters and Arguments
- [ ] Check out [[Session06 Additional Info]]

---
### Modifying `TransportationSearchForm`:
> Use this as a reference: [link](https://github.com/MehrdadShirvani/AlibabaClone-Frontend/tree/develop/alibabaclone-frontend/src/features/transportation)

- [ ] Remove Search Result fetching from inside the component
- [ ] Update `handleSearch` to Navigate with Parameters

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

- [ ] Used `useNavigate` from `react-router-dom`
```tsx
import { useNavigate } from "react-router-dom";
```
- [ ] Removed Result Display Section

## Add Routing 
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

---

## Setting Up Routing

- [ ] **Install React Router** (If you haven't already)

```bash
npm install react-router-dom
```
    
- [ ] **Wrap Your App in Router**

```jsx
<Router>
  <Navbar />
  <Routes>
    {/* your routes here */}
  </Routes>
</Router>
```

- [ ] **Define Routes**

```jsx
<Route path="/" element={<SearchPage />} />
<Route path="/:vehicleId/:fromCityId/:toCityId" element={<SearchResultsPage />} />
```
## 🚧Merge
- [ ] Create a PR and merge the current branch with develop

# 🧠 Hints & Notes
# 🙌 Acknowledgements

- ChatGPT for snippet refinement and explanations
# 🔍 References




