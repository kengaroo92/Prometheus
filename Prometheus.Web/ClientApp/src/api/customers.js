import axios from 'axios';

const baseUrl = 'https://localhost:7015/api/customers';

export async function getAllCustomers() {
    try {
        const response = await axios.get(baseUrl);
        return response.data;
    } catch (error) {
        console.error('Error fetching customers:', error);
        return [];
    }
}