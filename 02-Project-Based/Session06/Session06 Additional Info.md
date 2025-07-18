## `SearchResultsPage` 

This page is responsible for:
1. **Reading route parameters and query strings from the URL**
2. **Sending those values as a form to the backend**
3. **Showing the result (or loading/error message)**
    

---
### ✅ 1. **Route Parameters**
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

###  ✅ 2. **Query String Parameters**
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

###  ✅ 3. **Parsing and Converting Values**

React Router gives you everything as strings. So:

```js
const vehicleTypeId = vehicleId ? parseInt(vehicleId, 10) : 1;
const fromId = fromCityId ? parseInt(fromCityId, 10) : undefined;
const toId = toCityId ? parseInt(toCityId, 10) : undefined;
```

This ensures you have **numbers**, not strings, when building your form object.

---

###  ✅ 4. **Building the Search Form and Fetching Data**

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
