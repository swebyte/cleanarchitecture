// wwwroot/indexedDBFunctions.js

// Open a connection to the IndexedDB database
const openDatabase = async () => {
    return new Promise((resolve, reject) => {
        const request = window.indexedDB.open("db", 1);

        request.onerror = () => reject("Error opening database");
        request.onsuccess = () => resolve(request.result);

        request.onupgradeneeded = event => {
            const db = event.target.result;
            db.createObjectStore("products", { keyPath: "id" });
        };
    });
};

window.indexedDBFunctions = {};

// Function to store data in IndexedDB
window.indexedDBFunctions.storeData = async (storeName, data) => {
    try {
        const db = await openDatabase();
        const transaction = db.transaction(storeName, "readwrite");
        const objectStore = transaction.objectStore(storeName);

        // Clear existing data
        objectStore.clear();

        // Store new data
        for (const item of data) {
            objectStore.add(item);
        }

        return true;
    } catch (error) {
        console.error("Error storing data in IndexedDB:", error);
        return false;
    }
};

// Function to retrieve data from IndexedDB
window.indexedDBFunctions.getData = async (storeName) => {
    try {
        const db = await openDatabase();
        const transaction = db.transaction(storeName, "readonly");
        const objectStore = transaction.objectStore(storeName);
        const request = objectStore.getAll();

        return new Promise((resolve, reject) => {
            request.onerror = () => reject("Error retrieving data");
            request.onsuccess = () => resolve(request.result);
        });
    } catch (error) {
        console.error("Error retrieving data from IndexedDB:", error);
        return null;
    }
};
