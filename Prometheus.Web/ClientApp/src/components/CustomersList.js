import React, { useEffect, useState } from 'react';
import { getAllCustomers } from '../api/customers';

function CustomersList() {
    // Store the customers list. Initially it's empty.
    const [customers, setCustomers] = useState([]);

    // Fetch the customers list from the API.
    useEffect(() => {
        async function fetchData() {
            const fetchedCustomers = await getAllCustomers();
            setCustomers(fetchedCustomers);
        }

        fetchData();
    }, []);

    // Render the component
    return (
        <div>
            <h2>Customers</h2>
            <ul>
                {customers.map((customer) => (
                    <li key={customer.id}>{customer.name}</li>
                ))}
            </ul>
        </div>
    );
}
export default CustomersList;