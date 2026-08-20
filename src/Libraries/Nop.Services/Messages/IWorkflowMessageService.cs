using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Messages;

/// <summary>
/// Workflow message service
/// </summary>
public partial interface IWorkflowMessageService
{
    #region Customer workflow

    /// <summary>
    /// Sends 'Failed login attempt' notification message to a customer
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendCustomerFailedLoginAttemptNotificationAsync(Customer customer, long languageId);

    /// <summary>
    /// Sends 'New customer' notification message to a store owner
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendCustomerRegisteredStoreOwnerNotificationMessageAsync(Customer customer, long languageId);

    /// <summary>
    /// Sends a welcome message to a customer
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendCustomerWelcomeMessageAsync(Customer customer, long languageId);

    /// <summary>
    /// Sends an email validation message to a customer
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendCustomerEmailValidationMessageAsync(Customer customer, long languageId);

    /// <summary>
    /// Sends an email re-validation message to a customer
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendCustomerEmailRevalidationMessageAsync(Customer customer, long languageId);

    /// <summary>
    /// Sends password recovery message to a customer
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendCustomerPasswordRecoveryMessageAsync(Customer customer, long languageId);

    /// <summary>
    /// Sends 'New request to delete customer' message to a store owner
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendDeleteCustomerRequestStoreOwnerNotificationAsync(Customer customer, long languageId);

    #endregion

    #region Order workflow

    /// <summary>
    /// Sends an order placed notification to a vendor
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="vendor">Vendor instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPlacedVendorNotificationAsync(Order order, Vendor vendor, long languageId);

    /// <summary>
    /// Sends an order placed notification to a store owner
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPlacedStoreOwnerNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order placed notification to an affiliate
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPlacedAffiliateNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order paid notification to a store owner
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPaidStoreOwnerNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order paid notification to a customer
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="attachmentFilePath">Attachment file path</param>
    /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPaidCustomerNotificationAsync(Order order, long languageId,
        string attachmentFilePath = null, string attachmentFileName = null);

    /// <summary>
    /// Sends an order paid notification to a vendor
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="vendor">Vendor instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPaidVendorNotificationAsync(Order order, Vendor vendor, long languageId);

    /// <summary>
    /// Sends an order paid notification to an affiliate
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPaidAffiliateNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order placed notification to a customer
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="attachmentFilePath">Attachment file path</param>
    /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderPlacedCustomerNotificationAsync(Order order, long languageId,
        string attachmentFilePath = null, string attachmentFileName = null);

    /// <summary>
    /// Sends a shipment sent notification to a customer
    /// </summary>
    /// <param name="shipment">Shipment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendShipmentSentCustomerNotificationAsync(Shipment shipment, long languageId);

    /// <summary>
    /// Sends a shipment ready for pickup notification to a customer
    /// </summary>
    /// <param name="shipment">Shipment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendShipmentReadyForPickupNotificationAsync(Shipment shipment, long languageId);

    /// <summary>
    /// Sends a shipment delivered notification to a customer
    /// </summary>
    /// <param name="shipment">Shipment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendShipmentDeliveredCustomerNotificationAsync(Shipment shipment, long languageId);

    /// <summary>
    /// Sends an order processing notification to a customer
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="attachmentFilePath">Attachment file path</param>
    /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderProcessingCustomerNotificationAsync(Order order, long languageId,
        string attachmentFilePath = null, string attachmentFileName = null);

    /// <summary>
    /// Sends an order completed notification to a customer
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="attachmentFilePath">Attachment file path</param>
    /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderCompletedCustomerNotificationAsync(Order order, long languageId,
        string attachmentFilePath = null, string attachmentFileName = null);

    /// <summary>
    /// Sends an order completed notification to a store owner
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderCompletedStoreOwnerNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order cancelled notification to a customer
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderCancelledCustomerNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order cancelled notification to a vendor
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="vendor">Vendor instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderCancelledVendorNotificationAsync(Order order, Vendor vendor, long languageId);

    /// <summary>
    /// Sends an order cancelled notification to a store owner
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderCancelledStoreOwnerNotificationAsync(Order order, long languageId);

    /// <summary>
    /// Sends an order refunded notification to a store owner
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="refundedAmount">Amount refunded</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderRefundedStoreOwnerNotificationAsync(Order order, decimal refundedAmount, long languageId);

    /// <summary>
    /// Sends an order refunded notification to a customer
    /// </summary>
    /// <param name="order">Order instance</param>
    /// <param name="refundedAmount">Amount refunded</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendOrderRefundedCustomerNotificationAsync(Order order, decimal refundedAmount, long languageId);

    /// <summary>
    /// Sends a new order note added notification to a customer
    /// </summary>
    /// <param name="orderNote">Order note</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewOrderNoteAddedCustomerNotificationAsync(OrderNote orderNote, long languageId);

    /// <summary>
    /// Sends a "Recurring payment cancelled" notification to a store owner
    /// </summary>
    /// <param name="recurringPayment">Recurring payment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendRecurringPaymentCancelledStoreOwnerNotificationAsync(RecurringPayment recurringPayment, long languageId);

    /// <summary>
    /// Sends a "Recurring payment cancelled" notification to a customer
    /// </summary>
    /// <param name="recurringPayment">Recurring payment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendRecurringPaymentCancelledCustomerNotificationAsync(RecurringPayment recurringPayment, long languageId);

    /// <summary>
    /// Sends a "Recurring payment failed" notification to a customer
    /// </summary>
    /// <param name="recurringPayment">Recurring payment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendRecurringPaymentFailedCustomerNotificationAsync(RecurringPayment recurringPayment, long languageId);

    /// <summary>
    /// Sends a "Next recurring payment notification" message to a customer
    /// </summary>
    /// <param name="recurringPayment">Recurring payment</param>
    /// <param name="delayBeforeSend">Delay before send</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNextRecurringPaymentNotificationCustomerMessageAsync(RecurringPayment recurringPayment, int delayBeforeSend, long languageId);

    #endregion

    #region Newsletter workflow

    /// <summary>
    /// Sends a newsletter subscription activation message
    /// </summary>
    /// <param name="subscription">Newsletter subscription</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewsLetterSubscriptionActivationMessageAsync(NewsLetterSubscription subscription);

    /// <summary>
    /// Sends a newsletter subscription deactivation message
    /// </summary>
    /// <param name="subscription">Newsletter subscription</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewsLetterSubscriptionDeactivationMessageAsync(NewsLetterSubscription subscription);

    #endregion

    #region Send a message to a friend

    /// <summary>
    /// Sends "email a friend" message
    /// </summary>
    /// <param name="customer">Customer instance</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="product">Product instance</param>
    /// <param name="customerEmail">Customer's email</param>
    /// <param name="friendsEmail">Friend's email</param>
    /// <param name="personalMessage">Personal message</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendProductEmailAFriendMessageAsync(Customer customer, long languageId,
        Product product, string customerEmail, string friendsEmail, string personalMessage);

    /// <summary>
    /// Sends wishlist "email a friend" message
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="customerEmail">Customer's email</param>
    /// <param name="friendsEmail">Friend's email</param>
    /// <param name="personalMessage">Personal message</param>
    /// <param name="wishlistUrl">Wishlist URL</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendWishlistEmailAFriendMessageAsync(Customer customer, long languageId,
        string customerEmail, string friendsEmail, string personalMessage, string wishlistUrl);

    #endregion

    #region Return requests

    /// <summary>
    /// Sends 'New Return Request' message to a store owner
    /// </summary>
    /// <param name="returnRequest">Return request</param>
    /// <param name="orderItem">Order item</param>
    /// <param name="order">Order</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewReturnRequestStoreOwnerNotificationAsync(ReturnRequest returnRequest, OrderItem orderItem, Order order, long languageId);

    /// <summary>
    /// Sends 'New Return Request' message to a customer
    /// </summary>
    /// <param name="returnRequest">Return request</param>
    /// <param name="orderItem">Order item</param>
    /// <param name="order">Order</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewReturnRequestCustomerNotificationAsync(ReturnRequest returnRequest, OrderItem orderItem, Order order);

    /// <summary>
    /// Sends 'Return Request status changed' message to a customer
    /// </summary>
    /// <param name="returnRequest">Return request</param>
    /// <param name="orderItem">Order item</param>
    /// <param name="order">Order</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendReturnRequestStatusChangedCustomerNotificationAsync(ReturnRequest returnRequest, OrderItem orderItem, Order order);

    /// <summary>
    /// Sends 'Withdrawal request confirmation' message to a customer
    /// </summary>
    /// <param name="order">Order</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendWithdrawalRequestConfirmationNotificationAsync(Order order);
    
    #endregion

    #region Messages

    /// <summary>
    /// Sends a private message notification
    /// </summary>
    /// <param name="privateMessage">Private message</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendPrivateMessageNotificationAsync(PrivateMessage privateMessage, long languageId);

    #endregion

    #region Misc

    /// <summary>
    /// Sends 'New vendor account submitted' message to a store owner
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="vendor">Vendor</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewVendorAccountApplyStoreOwnerNotificationAsync(Customer customer, Vendor vendor, long languageId);

    /// <summary>
    /// Sends 'Vendor information change' message to a store owner
    /// </summary>
    /// <param name="vendor">Vendor</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendVendorInformationChangeStoreOwnerNotificationAsync(Vendor vendor, long languageId);

    /// <summary>
    /// Sends a product review notification message to a store owner
    /// </summary>
    /// <param name="productReview">Product review</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendProductReviewStoreOwnerNotificationMessageAsync(ProductReview productReview, long languageId);

    /// <summary>
    /// Sends a product review reply notification message to a customer
    /// </summary>
    /// <param name="productReview">Product review</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendProductReviewReplyCustomerNotificationMessageAsync(ProductReview productReview, long languageId);

    /// <summary>
    /// Sends a gift card notification
    /// </summary>
    /// <param name="giftCard">Gift card</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendGiftCardNotificationAsync(GiftCard giftCard, long languageId);

    /// <summary>
    /// Sends a "quantity below" notification to a store owner
    /// </summary>
    /// <param name="product">Product</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendQuantityBelowStoreOwnerNotificationAsync(Product product, long languageId);

    /// <summary>
    /// Sends a "quantity below" notification to a store owner
    /// </summary>
    /// <param name="combination">Attribute combination</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendQuantityBelowStoreOwnerNotificationAsync(ProductAttributeCombination combination, long languageId);

    /// <summary>
    /// Sends a "quantity below" notification to a vendor
    /// </summary>
    /// <param name="product">Product</param>
    /// <param name="vendor">Vendor</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendQuantityBelowVendorNotificationAsync(Product product, Vendor vendor, long languageId);

    /// <summary>
    /// Sends a "quantity below" notification to a vendor
    /// </summary>
    /// <param name="combination">Attribute combination</param>
    /// <param name="vendor">Vendor</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendQuantityBelowVendorNotificationAsync(ProductAttributeCombination combination, Vendor vendor, long languageId);

    /// <summary>
    /// Sends a "new VAT submitted" notification to a store owner
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="vatName">Received VAT name</param>
    /// <param name="vatAddress">Received VAT address</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendNewVatSubmittedStoreOwnerNotificationAsync(Customer customer, string vatName, string vatAddress, long languageId);

    /// <summary>
    /// Sends a blog comment notification message to a store owner
    /// </summary>
    /// <param name="blogComment">Blog comment</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendBlogCommentStoreOwnerNotificationMessageAsync(BlogComment blogComment, long languageId);

    /// <summary>
    /// Sends a 'Back in stock' notification message to a customer
    /// </summary>
    /// <param name="subscription">Subscription</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendBackInStockNotificationAsync(BackInStockSubscription subscription, long languageId);

    /// <summary>
    /// Sends "contact us" message
    /// </summary>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="senderEmail">Sender email</param>
    /// <param name="senderName">Sender name</param>
    /// <param name="subject">Email subject. Pass null if you want a message template subject to be used.</param>
    /// <param name="body">Email body</param>
    /// <param name="customAttributes">Custom attributes</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendContactUsMessageAsync(long languageId, string senderEmail, string senderName, string subject, string body, IDictionary<string, string> customAttributes);

    /// <summary>
    /// Sends "contact vendor" message
    /// </summary>
    /// <param name="vendor">Vendor</param>
    /// <param name="languageId">Message language identifier</param>
    /// <param name="senderEmail">Sender email</param>
    /// <param name="senderName">Sender name</param>
    /// <param name="subject">Email subject. Pass null if you want a message template subject to be used.</param>
    /// <param name="body">Email body</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<IList<long>> SendContactVendorMessageAsync(Vendor vendor, long languageId, string senderEmail, string senderName, string subject, string body);

    /// <summary>
    /// Sends a test email
    /// </summary>
    /// <param name="messageTemplateId">Message template identifier</param>
    /// <param name="sendToEmail">Send to email</param>
    /// <param name="tokens">Tokens</param>
    /// <param name="languageId">Message language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<long> SendTestEmailAsync(long messageTemplateId, string sendToEmail, List<Token> tokens, long languageId);

    #endregion

    #region Common

    /// <summary>
    /// Get active message templates by the name
    /// </summary>
    /// <param name="messageTemplateName">Message template name</param>
    /// <param name="storeId">Store identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the list of message templates
    /// </returns>
    Task<IList<MessageTemplate>> GetActiveMessageTemplatesAsync(string messageTemplateName, long storeId);

    /// <summary>
    /// Get email account to use with a message templates
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="languageId">Language identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the email account
    /// </returns>
    Task<EmailAccount> GetEmailAccountOfMessageTemplateAsync(MessageTemplate messageTemplate, long languageId);

    /// <summary>
    /// Ensure language is active
    /// </summary>
    /// <param name="languageId">Language identifier</param>
    /// <param name="storeId">Store identifier</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the active language identifier
    /// </returns>
    Task<long> EnsureLanguageIsActiveAsync(long languageId, long storeId);

    /// <summary>
    /// Get email and name to send email for store owner
    /// </summary>
    /// <param name="messageTemplateEmailAccount">Message template email account</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the email address and name to send email for store owner
    /// </returns>
    Task<(string email, string name)> GetStoreOwnerNameAndEmailAsync(EmailAccount messageTemplateEmailAccount);

    /// <summary>
    /// Get email and name to set ReplyTo property of email from customer 
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="customer">Customer</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the email address and name to reply
    /// </returns>
    Task<(string email, string name)> GetCustomerReplyToNameAndEmailAsync(MessageTemplate messageTemplate, Customer customer);

    /// <summary>
    /// Get email and name to set ReplyTo property of email from order
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="order">Order</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the email address and name to reply
    /// </returns>
    Task<(string email, string name)> GetCustomerReplyToNameAndEmailAsync(MessageTemplate messageTemplate, Order order);

    /// <summary>
    /// Send notification
    /// </summary>
    /// <param name="messageTemplate">Message template</param>
    /// <param name="emailAccount">Email account</param>
    /// <param name="languageId">Language identifier</param>
    /// <param name="tokens">Tokens</param>
    /// <param name="toEmailAddress">Recipient email address</param>
    /// <param name="toName">Recipient name</param>
    /// <param name="attachmentFilePath">Attachment file path</param>
    /// <param name="attachmentFileName">Attachment file name</param>
    /// <param name="replyToEmailAddress">"Reply to" email</param>
    /// <param name="replyToName">"Reply to" name</param>
    /// <param name="fromEmail">Sender email. If specified, then it overrides passed "emailAccount" details</param>
    /// <param name="fromName">Sender name. If specified, then it overrides passed "emailAccount" details</param>
    /// <param name="subject">Subject. If specified, then it overrides subject of a message template</param>
    /// <param name="ignoreDelayBeforeSend">A value indicating whether to ignore the delay before sending message</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifier
    /// </returns>
    Task<long> SendNotificationAsync(MessageTemplate messageTemplate,
        EmailAccount emailAccount, long languageId, IList<Token> tokens,
        string toEmailAddress, string toName,
        string attachmentFilePath = null, string attachmentFileName = null,
        string replyToEmailAddress = null, string replyToName = null,
        string fromEmail = null, string fromName = null, string subject = null,
        bool ignoreDelayBeforeSend = false);

    #endregion

    #region Reminders

    /// <summary>
    /// Sends a registration activation follow up to a customer
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifiers
    /// </returns>
    Task<IList<long>> SendIncompleteRegistrationNotificationMessageAsync(Customer customer);

    /// <summary>
    /// Sends an abandoned cart follow up to a customer
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="cart">Shopping cart</param>
    /// <param name="messageTemplateName">Follow up message name</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifiers
    /// </returns>
    Task<IList<long>> SendAbandonedCartFollowUpCustomerNotificationAsync(Customer customer,
        IList<ShoppingCartItem> cart, string messageTemplateName);

    /// <summary>
    /// Sends a pending order follow up to a customer
    /// </summary>
    /// <param name="customer">Customer</param>
    /// <param name="order">Order</param>
    /// <param name="messageTemplateName">Follow up message name</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the queued email identifiers
    /// </returns>
    Task<IList<long>> SendPendingOrderFollowUpCustomerNotificationAsync(Customer customer, Order order, string messageTemplateName);

    #endregion
}